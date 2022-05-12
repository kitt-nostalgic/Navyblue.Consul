using Navyblue.BaseLibrary;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul;

using ConsulHttpResponse = ConsulHttpResponse;

/// <summary>
///
/// </summary>
public sealed class OperationException : ConsulException
{
    public int StatusCode { get; }
    public string? StatusMessage { get; }
    public string StatusContent { get; }

    public OperationException(int statusCode, string? statusMessage, string statusContent) : base("OperationException(statusCode=" + statusCode + ", statusMessage='" + statusMessage + "', statusContent='" + statusContent + "')")
    {
        this.StatusCode = statusCode;
        this.StatusMessage = statusMessage;
        this.StatusContent = statusContent;
    }

    public OperationException(ConsulHttpResponse httpResponse) : this(httpResponse.StatusCode, httpResponse.StatusMessage, httpResponse.Content)
    {
    }

    public override string ToString()
    {
        return this.ToJson();
    }
}