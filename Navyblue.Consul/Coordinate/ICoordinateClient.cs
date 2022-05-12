using Navyblue.Consul.Coordinate.Model;

namespace Navyblue.Consul.Coordinate;

/// <summary>
///
/// </summary>
public interface ICoordinateClient
{
    ConsulResponse<IList<Datacenter>> Datacenters { get; }

    ConsulResponse<IList<Node>> GetNodes(QueryParams? queryParams);
}