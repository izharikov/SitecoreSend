using System.Net.Http;

namespace SitecoreSend.SDK;

public class MailingListService : BaseApiService, IMailingListService
{
    public Task<SendResponse<MailingListsResponse>?> GetAll(bool withStatistics = true,
        SortBy sortBy = SortBy.CreatedOn,
        SortMethod sortMethod = SortMethod.ASC, CancellationToken? cancellationToken = null)
    {
        return GetAll<MailingListsResponse>(withStatistics, sortBy, sortMethod,
            cancellationToken);
    }

    public Task<SendResponse<TResponse>?> GetAll<TResponse>(bool withStatistics = true,
        SortBy sortBy = SortBy.CreatedOn,
        SortMethod sortMethod = SortMethod.ASC, CancellationToken? cancellationToken = null)
    {
        var url = Url("lists", "ShortBy", sortBy, "SortMethod", sortMethod, "WithStatistics", withStatistics);
        return Get<SendResponse<TResponse>>(url, cancellationToken);
    }

    public Task<SendResponse<MailingListsResponse>?> GetWithPaging(int page, int pageSize,
        bool withStatistics = true, SortBy sortBy = SortBy.CreatedOn,
        SortMethod sortMethod = SortMethod.ASC, CancellationToken? cancellationToken = null)
    {
        return GetWithPaging<MailingListsResponse>(page, pageSize, withStatistics, sortBy, sortMethod,
            cancellationToken);
    }

    public Task<SendResponse<TResponse>?> GetWithPaging<TResponse>(int page, int pageSize,
        bool withStatistics = true,
        SortBy sortBy = SortBy.CreatedOn, SortMethod sortMethod = SortMethod.ASC,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{page}/{pageSize}", "ShortBy", sortBy, "SortMethod", sortMethod, "WithStatistics",
            withStatistics);
        return Get<SendResponse<TResponse>>(url, cancellationToken);
    }

    public Task<SendResponse<MailingList>?> Get(Guid id, bool withStatistics = true,
        CancellationToken? cancellationToken = null)
    {
        return Get<MailingList>(id, withStatistics, cancellationToken);
    }

    public Task<SendResponse<TResponse>?> Get<TResponse>(Guid id, bool withStatistics = true,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{id}/details", "WithStatistics", withStatistics);
        return Get<SendResponse<TResponse>>(url, cancellationToken);
    }

    public Task<SendResponse<Guid>?> Create(MailingListRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url("lists/create");
        return Post<SendResponse<Guid>>(url, request, cancellationToken);
    }

    public Task<SendResponse<Guid>?> Update(Guid id, MailingListRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{id}/update");
        return Post<SendResponse<Guid>>(url, request, cancellationToken);
    }

    public Task<SendResponse?> Delete(Guid id, CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{id}/delete");
        return Delete<SendResponse>(url, cancellationToken);
    }

    public Task<SendResponse<Guid?>?> CreateCustomField(Guid mailingListId, CustomFieldDefinitionRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{mailingListId}/customfields/create");
        return Post<SendResponse<Guid?>>(url, request, cancellationToken);
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

    public MailingListService(ApiConfiguration apiConfiguration, Func<HttpClient?>? httpClientFactory = null,
        bool disposeHttpClient = false) : base(apiConfiguration, httpClientFactory, disposeHttpClient)
    {
    }
}