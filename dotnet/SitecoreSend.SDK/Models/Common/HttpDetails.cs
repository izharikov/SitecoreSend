using System.Net;
using System.Net.Http;

namespace SitecoreSend.SDK;

public class HttpDetails
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Reason { get; set; }
    public bool IsSuccess { get; set; }

    public HttpDetails(HttpResponseMessage message)
    {
        StatusCode = message.StatusCode;
        Reason = message.ReasonPhrase;
        IsSuccess = (int) message.StatusCode >= 200 && (int) message.StatusCode <= 299;
    }
}