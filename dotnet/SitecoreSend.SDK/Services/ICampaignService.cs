namespace SitecoreSend.SDK;

/// <summary>
/// Use to fetch your campaigns, get details about campaigns, fetch your campaign senders and their details, create, clone, update, delete, test, and send campaigns. The API also lets you fetch campaign statistics, activity by location, link activity, and campaign summaries.
/// </summary>
public interface ICampaignService : IDisposable
{
    /// <summary>
    /// Retrieves a list of all campaigns in your Sitecore Send account with detailed information. Because the results for this call could be quite big, you can add paging and sorting information as inputs.
    /// </summary>
    /// <param name="page">The page number to display results for. Returns the first page if not specified.</param>
    /// <param name="pageSize">The maximum number of results per page. This must be a positive integer up to 100. Returns 10 results per page if not specified. If a value greater than 100 is specified, it is treated as 100.</param>
    /// <param name="sortBy">The name of the campaign property to sort results by.</param>
    /// <param name="sortMethod">Specifies the method to sort results.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<CampaignListResponse>?> GetAll(int page, int pageSize, SortBy sortBy = SortBy.CreatedOn,
        SortMethod sortMethod = SortMethod.ASC,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves the details about a specific campaign in your Sitecore Send account. No statistics are included in the result.
    /// </summary>
    /// <param name="campaignId">The ID of the campaign that contains the details you are requesting.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<Campaign>?> Get(Guid campaignId, CancellationToken? cancellationToken = null);
}