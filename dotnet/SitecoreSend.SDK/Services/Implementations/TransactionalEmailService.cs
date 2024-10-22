using System.Net.Http;
using SitecoreSend.SDK.Transactional;

namespace SitecoreSend.SDK;

public class TransactionalEmailService : BaseApiService, ITransactionalService
{
    public TransactionalEmailService(ApiConfiguration apiConfiguration, Func<HttpClient?>? httpClientFactory) : base(apiConfiguration, httpClientFactory)
    {
    }
    
    public Task<SendResponse<TransactionalResult>?> Send(EmailRequest request, CancellationToken? cancellationToken = null)
    {
        var url = Url("campaigns/transactional/send");
        return Post<SendResponse<TransactionalResult>>(url, request, cancellationToken);
    }
}