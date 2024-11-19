using SitecoreSend.SDK.Tools;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public class SegmentsServiceTest(ITestOutputHelper testOutputHelper)
{
    private readonly ISendClient _send = TestsApp.SendFactory(testOutputHelper);

    [Fact]
    public async Task Segments_OnGetSegments_ShouldReturnResult()
    {
        var listId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TestListId").Value!);
        var response = await _send.Segments.GetAll(listId);

        Assert.True(response?.Success);
        Assert.NotNull(response?.Data);

        foreach (var segment in response.Data.Segments)
        {
            var segmentResponse = await _send.Segments.Get(listId, segment.ID);

            Assert.True(segmentResponse?.Success);
            Assert.NotNull(segmentResponse?.Data);

            var segmentSubscribers = await _send.Segments.GetSubscribers(listId, segment.ID);
            Assert.True(segmentSubscribers?.Success);
            Assert.NotNull(segmentSubscribers?.Data);
        }
    }

    [Fact]
    public async Task Segments_OnCreateEmptyAndDelete_ShouldPerformOperations()
    {
        var listId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TestListId").Value!);

        var emptySegment = await _send.Segments.Create(listId, new SegmentRequest()
        {
            Name = "Empty Segment",
        });

        Assert.True(emptySegment?.Success);
        Assert.NotNull(emptySegment?.Data);
        Assert.True(emptySegment.Data > 0);

        var segmentId = emptySegment.Data.Value;

        var updateSegment = await _send.Segments.Update(listId, segmentId, new SegmentRequest()
        {
            Name = "Empty Segment #2",
        });

        Assert.True(updateSegment?.Success);

        var segment = await _send.Segments.Get(listId, segmentId);
        Assert.True(segment?.Success);
        Assert.Equal("Empty Segment #2", segment?.Data?.Name);
        Assert.Empty(segment?.Data?.Criteria ?? []);

        var deleteResponse = await _send.Segments.Delete(listId, segmentId);
        Assert.True(deleteResponse?.Success);

        segment = await _send.Segments.Get(listId, segmentId);
        Assert.False(segment?.Success);
    }

    [Fact]
    public async Task Segments_OnCriteriaOperations_ShouldPerform()
    {
        var listId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TestListId").Value!);

        var segment = await _send.Segments.Create(listId, new SegmentRequest()
        {
            Name = "Criteria Test",
            MatchType = MatchType.Any,
            FetchType = SegmentFetchType.TopPercent,
            FetchValue = 50,
            Criteria =
            [
                new CriteriaRequest()
                {
                    Field = CriteriaType.DateAdded,
                    Comparer = ComparerCriteria.IsAfter,
                    Value = SegmentValueHelper.DateValue(DateTimeOffset.Now.AddMonths(-12)),
                },
                new CriteriaRequest()
                {
                    Field = CriteriaType.SubscribeMethod,
                    Comparer = ComparerCriteria.Is,
                    Value = SegmentValueHelper.EnumValue(SubscribeMethod.Api),
                    DateFrom = DateTimeOffset.Now.AddMonths(-2),
                    DateTo = DateTimeOffset.Now.AddMonths(2),
                },
            ],
        });

        Assert.True(segment?.Success);
        Assert.True(segment?.Data > 0);

        var segmentId = segment.Data.Value;

        var result = await _send.Segments.Get(listId, segmentId);

        Assert.True(result?.Success);
        Assert.True(result?.Data?.Criteria.Count == 2);
        Assert.True(result.Data.Criteria[0].Comparer == ComparerCriteria.IsAfter);
        Assert.True(result.Data.Criteria[0].Field == CriteriaType.DateAdded);

        Assert.True(result.Data.Criteria[1].Comparer == ComparerCriteria.Is);
        Assert.True(result.Data.Criteria[1].Field == CriteriaType.SubscribeMethod);

        var subscribeMethods = SegmentValueHelper.Parse<SubscribeMethod>(result.Data.Criteria[1].Value);
        Assert.Single(subscribeMethods);
        Assert.Equal(SubscribeMethod.Api, subscribeMethods[0]);

        var addCriteria = await _send.Segments.AddCriteria(listId, segmentId, new CriteriaRequest()
        {
            Field = CriteriaType.MemberTag,
            Comparer = ComparerCriteria.Is,
            Value = SegmentValueHelper.ListValue("tag1", "tag2"),
        });

        Assert.True(addCriteria?.Success);
        Assert.True(addCriteria?.Data > 0);

        result = await _send.Segments.Get(listId, segmentId);
        Assert.True(result?.Success);
        Assert.True(result?.Data?.Criteria.Count == 3);
        Assert.True(result.Data.Criteria[2].Comparer == ComparerCriteria.Is);
        Assert.True(result.Data.Criteria[2].Field == CriteriaType.MemberTag);

        var tags = SegmentValueHelper.Parse(result.Data.Criteria[2].Value);
        Assert.True(tags.Count == 2);
        Assert.True(tags[0] == "tag1");
        Assert.True(tags[1] == "tag2");

        var updateCriteria = await _send.Segments.UpdateCriteria(listId, segmentId, addCriteria.Data.Value,
            new CriteriaRequest()
            {
                Field = CriteriaType.MemberTag,
                Comparer = ComparerCriteria.Is,
                Value = SegmentValueHelper.ListValue("tag1", "tag2", "tag3"),
            });

        Assert.True(updateCriteria?.Success);

        result = await _send.Segments.Get(listId, segmentId);
        Assert.True(result?.Success);
        Assert.NotNull(result?.Data);

        tags = SegmentValueHelper.Parse(result.Data.Criteria[2].Value);
        Assert.True(tags.Count == 3);
        Assert.True(tags[0] == "tag1");
        Assert.True(tags[1] == "tag2");
        Assert.True(tags[2] == "tag3");

        var deleteResponse = await _send.Segments.Delete(listId, segmentId);
        Assert.True(deleteResponse?.Success);
    }
}