namespace SitecoreSend.SDK;

/// <summary>
/// Use the Sitecore Send API to fetch your segments, fetch subscribers in a segment, get details about segments, create, update, add or update criteria, and delete segments.
/// </summary>
public interface ISegmentsService
{
    /// <summary>
    /// Retrieves a list of all segments including their criteria for a specific email list in your Sitecore Send account.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the segments.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<SegmentsResponse>?> GetAll(Guid listId, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves the details about a specific segment in a specific email list in your account. It includes the segment criteria but not the subscribers included in the segment.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the segment.</param>
    /// <param name="segmentId">The ID of the segment that contains the details you are requesting.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<Segment>?> Get(Guid listId, int segmentId, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves a list of subscribers that match the criteria of a specific segment.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the segment.</param>
    /// <param name="segmentId">The ID of the segment that contains the subscribers you are requesting.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<SubscribersResponse>?> GetSubscribers(Guid listId, int segmentId,
        CancellationToken? cancellationToken = null);

    Task<SendResponse<int>?> CreateEmpty(Guid listId, EmptySegmentRequest request,
        CancellationToken? cancellationToken = null);
}