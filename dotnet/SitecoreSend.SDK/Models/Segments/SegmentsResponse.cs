namespace SitecoreSend.SDK;

public class SegmentsResponse : BasePagingResponse
{
    public IList<Segment> Segments { get; set; } = [];
}