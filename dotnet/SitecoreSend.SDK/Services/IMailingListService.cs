namespace SitecoreSend.SDK;

/// <summary>
/// Use to get your email lists, get details about email lists, create, update, and delete email lists. You can also create, update, or remove custom fields in your email lists.
/// </summary>
public interface IMailingListService : IDisposable
{
    /// <summary>
    /// Retrieves a list of all the active email lists in your Sitecore Send account.
    /// </summary>
    /// <param name="withStatistics">Specifies whether to fetch statistics for the subscribers.</param>
    /// <param name="sortBy">The name of the email list property to sort results by.</param>
    /// <param name="sortMethod">Specifies the method to sort results.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<MailingListsResponse>?> GetAll(bool withStatistics = true,
        SortBy sortBy = SortBy.CreatedOn,
        SortMethod sortMethod = SortMethod.ASC, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves a list of all the active email lists in your Sitecore Send account.
    /// </summary>
    /// <param name="withStatistics">Specifies whether to fetch statistics for the subscribers.</param>
    /// <param name="sortBy">The name of the email list property to sort results by.</param>
    /// <param name="sortMethod">Specifies the method to sort results.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<TResponse>?> GetAll<TResponse>(bool withStatistics = true,
        SortBy sortBy = SortBy.CreatedOn,
        SortMethod sortMethod = SortMethod.ASC, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves a list of all the active email lists in your Sitecore Send account with paging information. Because the results for this call could be quite big, you can add paging information as input.
    /// </summary>
    /// <param name="page">The page that you want to get.</param>
    /// <param name="pageSize">The number of email lists per page.</param>
    /// <param name="withStatistics">Specifies whether to fetch statistics for the subscribers.</param>
    /// <param name="sortBy">The name of the email list property to sort results by.</param>
    /// <param name="sortMethod">Specifies the method to sort results.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<MailingListsResponse>?> GetWithPaging(int page, int pageSize,
        bool withStatistics = true, SortBy sortBy = SortBy.CreatedOn, SortMethod sortMethod = SortMethod.ASC,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves a list of all the active email lists in your Sitecore Send account with paging information. Because the results for this call could be quite big, you can add paging information as input.
    /// </summary>
    /// <param name="page">The page that you want to get.</param>
    /// <param name="pageSize">The number of email lists per page.</param>
    /// <param name="withStatistics">Specifies whether to fetch statistics for the subscribers.</param>
    /// <param name="sortBy">The name of the email list property to sort results by.</param>
    /// <param name="sortMethod">Specifies the method to sort results.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <typeparam name="TResponse">Custom type for response.</typeparam>
    /// <returns></returns>
    Task<SendResponse<TResponse>?> GetWithPaging<TResponse>(int page, int pageSize,
        bool withStatistics = true, SortBy sortBy = SortBy.CreatedOn, SortMethod sortMethod = SortMethod.ASC,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves the details about a specific email list in your account. You can include subscriber statistics in your results. Any segments existing in the requested email list are excluded from the results.
    /// </summary>
    /// <param name="id">The ID of the email list that contains the details you are requesting.</param>
    /// <param name="withStatistics">Specifies whether to fetch statistics for the subscribers.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<MailingList>?> Get(Guid id, bool withStatistics = true,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves the details about a specific email list in your account. You can include subscriber statistics in your results. Any segments existing in the requested email list are excluded from the results.
    /// </summary>
    /// <param name="id">The ID of the email list that contains the details you are requesting.</param>
    /// <param name="withStatistics">Specifies whether to fetch statistics for the subscribers.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <typeparam name="TResponse">Custom type for response.</typeparam>
    /// <returns></returns>
    Task<SendResponse<TResponse>?> Get<TResponse>(Guid id, bool withStatistics = true,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Creates a new empty email list in your account.
    /// </summary>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>Response object with the ID of the email list created.</returns>
    Task<SendResponse<Guid>?> Create(MailingListRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Updates the properties of an existing email list in your account. You can update the email list name, confirmation page, and redirect page URLs.
    /// </summary>
    /// <param name="id">The ID of the email list to be updated.</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>Response object with the ID of the updated email list.</returns>
    Task<SendResponse<Guid>?> Update(Guid id, MailingListRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Deletes an email list from your Sitecore Send account.
    /// </summary>
    /// <param name="id">The ID of the email list to be deleted.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> Delete(Guid id, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Creates a new custom field in a specific email list.
    /// </summary>
    /// <param name="mailingListId">The ID of the email list where the custom field is created.</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>Response object with the ID of the created custom field.</returns>
    Task<SendResponse<Guid?>?> CreateCustomField(Guid mailingListId, CustomFieldDefinitionRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Updates the properties of a custom field in a specific email list.
    /// </summary>
    /// <param name="mailingListId">The ID of the email list containing the custom field.</param>
    /// <param name="fieldId">The ID of the custom field to be updated.</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> UpdateCustomField(Guid mailingListId, Guid fieldId, CustomFieldDefinitionRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Removes a custom field from a specific email list.
    /// </summary>
    /// <param name="mailingListId">The ID of the email list containing the custom field.</param>
    /// <param name="fieldId">The ID of the custom field to be deleted.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> RemoveCustomField(Guid mailingListId, Guid fieldId,
        CancellationToken? cancellationToken = null);
}