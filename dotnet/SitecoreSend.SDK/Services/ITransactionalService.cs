using SitecoreSend.SDK.Transactional;

namespace SitecoreSend.SDK;

public interface ITransactionalService
{
    Task<SendResponse<TransactionalResult>?> Send(EmailRequest request, CancellationToken? cancellationToken = null);
}