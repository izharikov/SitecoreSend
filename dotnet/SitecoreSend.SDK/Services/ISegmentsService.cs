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
    /// <param name="page">The page of subscriber statistics results to return.</param>
    /// <param name="pageSize">The page size of subscriber statistics results to return.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<SubscribersResponse>?> GetSubscribers(Guid listId, int segmentId, int? page = null, int? pageSize = null,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Creates a new segment with criteria in a specific email list
    /// </summary>
    /// <param name="listId">The ID of the email list where the segment is created.</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<int?>?> Create(Guid listId, SegmentRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Updates the properties and criteria of an existing segment. You can update the segment's name and match type. If criteria are included in the segment, you can update the criteria but the existing name and settings are retained.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the segment.</param>
    /// <param name="segmentId">The ID of the segment to be updated.</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> Update(Guid listId, int segmentId, SegmentRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Adds a criterion or rule to a specific segment in your email list.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the segment.</param>
    /// <param name="segmentId">The ID of the segment.</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<int?>?> AddCriteria(Guid listId, int segmentId, CriteriaRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Updates an existing criterion or rule in a specific segment in your email list.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the segment.</param>
    /// <param name="segmentId">The ID of the segment.</param>
    /// <param name="criteriaId">The ID of the criteria of a segment to be updated.</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> UpdateCriteria(Guid listId, int segmentId, int criteriaId, CriteriaRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Deletes a segment including its criteria from a specific email list. Deleting a segment does not affect the email list subscribers.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the segment.</param>
    /// <param name="segmentId">The ID of the segment to be deleted.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> Delete(Guid listId, int segmentId, CancellationToken? cancellationToken = null);
}