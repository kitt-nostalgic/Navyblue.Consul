using Navyblue.BaseLibrary;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul.Status;

/// <summary>
///
/// </summary>
public sealed class StatusConsulClient : IStatusClient
{
    private readonly IConsulRawClient _consulRawClient;

    public StatusConsulClient(IConsulRawClient consulRawClient)
    {
        this._consulRawClient = consulRawClient;
    }

    public ConsulResponse<string> StatusLeader
    {
        get
        {
            ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/status/leader");

            if (httpResponse.StatusCode == 200)
            {
                string value = httpResponse.Content.FromJson<string>();
                return new ConsulResponse<string>(value, httpResponse);
            }

            throw new OperationException(httpResponse);
        }
    }

    public ConsulResponse<IList<string>> StatusPeers
    {
        get
        {
            ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/status/peers");

            if (httpResponse.StatusCode == 200)
            {
                IList<string> value = httpResponse.Content.FromJson<List<string>>();
                return new ConsulResponse<IList<string>>(value, httpResponse);
            }

            throw new OperationException(httpResponse);
        }
    }
}