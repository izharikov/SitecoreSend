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

    /// <summary>
    /// Retrieves a list of all the active campaign senders in your Sitecore Send account.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<IList<Sender>>?> GetAllSenders(CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves the details about a specific campaign sender using the sender's email address.
    /// </summary>
    /// <param name="email">The email address of the sender that contains the details you are requesting.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<Sender>?> GetSender(string email, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Creates a new draft campaign in your account that is ready for sending or testing. You can create either a regular campaign or an A/B split campaign. The campaign content must be specified from a web location or by pasting the complete HTML body of your campaign. If you are creating a regular campaign, you can ignore the A/B split campaign parameters.
    /// </summary>
    /// <param name="request">Campaign request</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<CampaignResponse>?> CreateDraft(CampaignRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Clones or creates an exact copy of an existing campaign in your Sitecore Send account. The cloned campaign is created with a Draft status.
    /// </summary>
    /// <param name="campaignId">The ID of the campaign that you want to clone.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<Campaign>?> Clone(Guid campaignId, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Deletes a draft or a sent campaign from your Sitecore Send account.
    /// </summary>
    /// <param name="campaignId">The ID of the draft or sent campaign that you want to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> Delete(Guid campaignId, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Updates the details of an existing draft campaign in your account. Non-draft campaigns cannot be updated. If you are updating a regular campaign, you can ignore the A/B split campaign parameters.
    /// </summary>
    /// <param name="campaignId">The ID of the campaign that you want to update.</param>
    /// <param name="request">Campaign request</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> UpdateDraft(Guid campaignId, CampaignRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Tests and previews a draft campaign by sending it to a list of email addresses.
    /// </summary>
    /// <param name="campaignId">The ID of the draft campaign that you want to test.</param>
    /// <param name="emails">A list of email addresses that you want to use to send your test campaign.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> SendTest(Guid campaignId, IList<string> emails, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Sends a draft campaign immediately to all recipients in the email list you've selected for the campaign.
    /// </summary>
    /// <param name="campaignId">The ID of the draft campaign that you want to send.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> Send(Guid campaignId, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Schedules the delivery of a campaign by assigning a specific date and time.
    /// </summary>
    /// <param name="campaignId">The ID of the campaign that you want to schedule.</param>
    /// <param name="date">The specific date and time the campaign is scheduled to be delivered.</param>
    /// <param name="timeZoneInfo">The time zone of your specified date and time. If you don't specify any timezone value, the time zone in your time and date settings is used.</param>
    /// <param name="format">Use the same format that you have in the Time and date settings in your account. For example, dd-mm-yyyy.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> Schedule(Guid campaignId, DateTimeOffset date, TimeZoneInfo? timeZoneInfo = null,
        string? format = "dd-mm-yyyy", CancellationToken? cancellationToken = null);

    /// <summary>
    /// Removes a previously scheduled date and time for delivering a campaign. If already queued, the campaign is sent immediately.
    /// </summary>
    /// <param name="campaignId">The ID of the campaign that you want to unschedule.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> Unschedule(Guid campaignId, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves a list of statistics for a specific campaign based on activity such as emails sent, opened, bounced, links clicked, and so on. You have the option to specify the date the activity occurred.
    /// </summary>
    /// <param name="campaignId">The ID of the campaign that you are fetching statistics for.</param>
    /// <param name="type">The type of activity used to get information and display statistics.</param>
    /// <param name="date">The specific year, month, and day the activity occurred. The date has a YYYY/MM/DD format.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<CampaignStatistics>?> GetStatistics(Guid campaignId, ActivityType type,
        DateTimeOffset? date = null,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves a list of statistics for a specific campaign based on activity such as emails sent, opened, bounced, links clicked, and so on. Because this call can return a large number of results, you can add paging information as input. You have the option to filter the results within a date range.
    /// </summary>
    /// <param name="campaignId">The ID of the campaign that you are fetching statistics for.</param>
    /// <param name="type">The type of activity used to get information and display statistics.</param>
    /// <param name="page">The page number to display results for. If not specified, the first page is returned.</param>
    /// <param name="pageSize">The maximum number of results per page. This must be a positive integer up to 100. Returns 50 results per page if not specified. If a value greater than 100 is specified, it is treated as 100. </param>
    /// <param name="from">The start date value to return results.  If not specified, results are returned from the date the campaign was sent. From date has a DD-MM-YYYY format.</param>
    /// <param name="to">The end date value to return results. If not specified, results are returned up to the current date.  To date has a DD-MM-YYYY format.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<CampaignStatistics>?> GetStatistics(Guid campaignId, ActivityType type, int page, int pageSize,
        DateTimeOffset? from = null, DateTimeOffset? to = null,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves a summary of results for a specific sent campaign. The summary includes information such as the number of recipients, opens, clicks, bounces, unsubscribes, forwards, and so on, to date.
    /// </summary>
    /// <param name="campaignId">The ID of the campaign that you want to get a summary of.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<CampaignSummary>?> GetSummary(Guid campaignId, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves the details about a specific campaign's unique or total opens by location.
    /// </summary>
    /// <param name="campaignId">The ID of the campaign that you want to get the activity by location of.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<CampaignStatistics>?> GetActivityByLocation(Guid campaignId,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves a list of all the links in a specific campaign and the number of unique or total link clicks made by campaign recipients.
    /// </summary>
    /// <param name="campaignId">The ID of the campaign that you want to get link activity by location of.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<CampaignStatistics>?> GetLinkActivity(Guid campaignId,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves a summary of results of a specific sent A/B campaign. The summary includes separate results for campaign versions A and B information, and information such as the number of recipients, opens, clicks, bounces, unsubscribes, forwards, and so on, to date.
    /// </summary>
    /// <param name="campaignId">The ID of the A/B campaign that you want to get a summary of.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<ABCampaignSummary>?> GetABCampaignSummary(Guid campaignId,
        CancellationToken? cancellationToken = null);
}