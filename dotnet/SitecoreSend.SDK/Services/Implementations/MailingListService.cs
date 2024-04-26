using System.Net.Http;

namespace SitecoreSend.SDK
{
    public class MailingListService : BaseApiService, IMailingListService
    {
        public MailingListService(ApiConfiguration apiConfiguration) : base(apiConfiguration)
        {
        }

        public MailingListService(ApiConfiguration apiConfiguration, HttpClient httpClient) : base(apiConfiguration,
            httpClient)
        {
        }

        public Task<SendResponse<MailingListsResponse>?> GetAllMailingLists(bool withStatistics = true,
            SortBy sortBy = SortBy.CreatedOn,
            SortMethod sortMethod = SortMethod.ASC, CancellationToken? cancellationToken = null)
        {
            return GetAllMailingLists<MailingListsResponse>(withStatistics, sortBy, sortMethod,
                cancellationToken);
        }

        public Task<SendResponse<TResponse>?> GetAllMailingLists<TResponse>(bool withStatistics = true,
            SortBy sortBy = SortBy.CreatedOn,
            SortMethod sortMethod = SortMethod.ASC, CancellationToken? cancellationToken = null)
        {
            var url = Url("lists", "ShortBy", sortBy, "SortMethod", sortMethod, "WithStatistics", withStatistics);
            return Get<SendResponse<TResponse>>(url, cancellationToken);
        }

        public Task<SendResponse<MailingListsResponse>?> GetMailingListsWithPaging(int page, int pageSize,
            bool withStatistics = true, SortBy sortBy = SortBy.CreatedOn,
            SortMethod sortMethod = SortMethod.ASC, CancellationToken? cancellationToken = null)
        {
            return GetMailingListsWithPaging<MailingListsResponse>(page, pageSize, withStatistics, sortBy, sortMethod,
                cancellationToken);
        }

        public Task<SendResponse<TResponse>?> GetMailingListsWithPaging<TResponse>(int page, int pageSize,
            bool withStatistics = true,
            SortBy sortBy = SortBy.CreatedOn, SortMethod sortMethod = SortMethod.ASC,
            CancellationToken? cancellationToken = null)
        {
            var url = Url($"lists/{page}/{pageSize}", "ShortBy", sortBy, "SortMethod", sortMethod, "WithStatistics",
                withStatistics);
            return Get<SendResponse<TResponse>>(url, cancellationToken);
        }

        public Task<SendResponse<MailingList>?> GetMailingList(Guid id, bool withStatistics = true,
            CancellationToken? cancellationToken = null)
        {
            return GetMailingList<MailingList>(id, withStatistics, cancellationToken);
        }

        public Task<SendResponse<TResponse>?> GetMailingList<TResponse>(Guid id, bool withStatistics = true,
            CancellationToken? cancellationToken = null)
        {
            var url = Url($"lists/{id}/details", "WithStatistics", withStatistics);
            return Get<SendResponse<TResponse>>(url, cancellationToken);
        }

        public Task<SendResponse<Guid>?> CreateMailingList(MailingListRequest request,
            CancellationToken? cancellationToken = null)
        {
            var url = Url("lists/create");
            return Post<SendResponse<Guid>>(url, request, cancellationToken);
        }

        public Task<SendResponse<Guid>?> UpdateMailingList(Guid id, MailingListRequest request,
            CancellationToken? cancellationToken = null)
        {
            var url = Url($"lists/{id}/update");
            return Post<SendResponse<Guid>>(url, request, cancellationToken);
        }

        public Task<SendResponse?> DeleteMailingList(Guid id, CancellationToken? cancellationToken = null)
        {
            var url = Url($"lists/{id}/delete");
            return Delete<SendResponse>(url, cancellationToken);
        }

        public Task<SendResponse<Guid>?> CreateCustomField(Guid mailingListId, CustomFieldDefinitionRequest request,
            CancellationToken? cancellationToken = null)
        {
            var url = Url($"lists/{mailingListId}/customfields/create");
            return Post<SendResponse<Guid>>(url, request, cancellationToken);
        }

        public Task<SendResponse?> UpdateCustomField(Guid mailingListId, Guid fieldId,
            CustomFieldDefinitionRequest request, CancellationToken? cancellationToken = null)
        {
            var url = Url($"lists/{mailingListId}/customfields/{fieldId}/update");
            return Post<SendResponse>(url, request, cancellationToken);
        }

        public Task<SendResponse?> RemoveCustomField(Guid mailingListId, Guid fieldId,
            CancellationToken? cancellationToken = null)
        {
            var url = Url($"lists/{mailingListId}/customfields/{fieldId}/delete");
            return Delete<SendResponse>(url, cancellationToken);
        }
    }
}