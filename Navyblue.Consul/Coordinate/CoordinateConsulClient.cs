using Navyblue.BaseLibrary;
using Navyblue.Consul.Coordinate.Model;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul.Coordinate;

/// <summary>
///
/// </summary>
public class CoordinateConsulClient : ICoordinateClient
{
    private readonly IConsulRawClient _consulRawClient;

    public CoordinateConsulClient(IConsulRawClient consulRawClient)
    {
        _consulRawClient = consulRawClient;
    }

    public virtual ConsulResponse<IList<Datacenter>> Datacenters
    {
        get
        {
            ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/coordinate/datacenters");

            if (httpResponse.StatusCode == 200)
            {
                IList<Datacenter> value = httpResponse.Content.FromJson<List<Datacenter>>();
                return new ConsulResponse<IList<Datacenter>>(value, httpResponse);
            }

            throw new OperationException(httpResponse);
        }
    }

    public virtual ConsulResponse<IList<Node>> GetNodes(QueryParams? queryParams)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/coordinate/nodes", queryParams);

        if (httpResponse.StatusCode == 200)
        {
            IList<Node> value = httpResponse.Content.FromJson<List<Node>>();
            return new ConsulResponse<IList<Node>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }
}