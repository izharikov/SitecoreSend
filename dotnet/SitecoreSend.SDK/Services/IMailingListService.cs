namespace SitecoreSend.SDK
{
    public interface IMailingListService
    {
        Task<SendResponse<MailingListsResponse>?> GetAllMailingLists(bool withStatistics = true,
            SortBy sortBy = SortBy.CreatedOn,
            SortMethod sortMethod = SortMethod.ASC, CancellationToken? cancellationToken = null);

        Task<SendResponse<TResponse>?> GetAllMailingLists<TResponse>(bool withStatistics = true,
            SortBy sortBy = SortBy.CreatedOn,
            SortMethod sortMethod = SortMethod.ASC, CancellationToken? cancellationToken = null);

        Task<SendResponse<MailingListsResponse>?> GetMailingListsWithPaging(int page, int pageSize,
            bool withStatistics = true, SortBy sortBy = SortBy.CreatedOn, SortMethod sortMethod = SortMethod.ASC,
            CancellationToken? cancellationToken = null);

        Task<SendResponse<TResponse>?> GetMailingListsWithPaging<TResponse>(int page, int pageSize,
            bool withStatistics = true, SortBy sortBy = SortBy.CreatedOn, SortMethod sortMethod = SortMethod.ASC,
            CancellationToken? cancellationToken = null);

        Task<SendResponse<MailingList>?> GetMailingList(Guid id, bool withStatistics = true,
            CancellationToken? cancellationToken = null);

        Task<SendResponse<TResponse>?> GetMailingList<TResponse>(Guid id, bool withStatistics = true,
            CancellationToken? cancellationToken = null);

        Task<SendResponse<Guid>?> CreateMailingList(MailingListRequest request,
            CancellationToken? cancellationToken = null);

        Task<SendResponse<Guid>?> UpdateMailingList(Guid id, MailingListRequest request,
            CancellationToken? cancellationToken = null);

        Task<SendResponse?> DeleteMailingList(Guid id, CancellationToken? cancellationToken = null);

        Task<SendResponse<Guid>?> CreateCustomField(Guid mailingListId, CustomFieldDefinitionRequest request,
            CancellationToken? cancellationToken = null);

        Task<SendResponse?> UpdateCustomField(Guid mailingListId, Guid fieldId, CustomFieldDefinitionRequest request,
            CancellationToken? cancellationToken = null);

        Task<SendResponse?> RemoveCustomField(Guid mailingListId, Guid fieldId,
            CancellationToken? cancellationToken = null);
    }
}