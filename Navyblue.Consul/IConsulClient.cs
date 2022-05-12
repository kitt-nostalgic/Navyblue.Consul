using Navyblue.Consul.ACL;
using Navyblue.Consul.Agent;
using Navyblue.Consul.Catalog;
using Navyblue.Consul.Coordinate;
using Navyblue.Consul.Event;
using Navyblue.Consul.Health;
using Navyblue.Consul.KV;
using Navyblue.Consul.Query;
using Navyblue.Consul.Session;
using Navyblue.Consul.Status;

namespace Navyblue.Consul;

public interface IConsulClient : IAclClient, IAgentClient, ICatalogClient, ICoordinateClient, IEventClient, IHealthClient, IKeyValueClient, IQueryClient, ISessionClient, IStatusClient
{

}