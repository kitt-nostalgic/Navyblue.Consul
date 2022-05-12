using Navyblue.Consul.ACL;
using Navyblue.Consul.ACL.Model;
using Navyblue.Consul.Agent;
using Navyblue.Consul.Agent.Model;
using Navyblue.Consul.Agent.Model.SelfModel;
using Navyblue.Consul.Catalog;
using Navyblue.Consul.Catalog.Model;
using Navyblue.Consul.Coordinate;
using Navyblue.Consul.Coordinate.Model;
using Navyblue.Consul.Event;
using Navyblue.Consul.Event.Model;
using Navyblue.Consul.Health;
using Navyblue.Consul.Health.Model;
using Navyblue.Consul.KV;
using Navyblue.Consul.KV.Model;
using Navyblue.Consul.Query;
using Navyblue.Consul.Query.Model;
using Navyblue.Consul.Session;
using Navyblue.Consul.Session.Model;
using Navyblue.Consul.Status;
using Check = Navyblue.Consul.Agent.Model.Check;
using Node = Navyblue.Consul.Catalog.Model.Node;
using IQueryClient = Navyblue.Consul.Query.IQueryClient;
using IStatusClient = Navyblue.Consul.Status.IStatusClient;

namespace Navyblue.Consul;

/// <summary>
/// </summary>
public class ConsulClient : IConsulClient
{
    private readonly IAclClient _aclClient;
    private readonly IAgentClient _agentClient;
    private readonly ICatalogClient _catalogClient;
    private readonly ICoordinateClient _coordinateClient;
    private readonly IEventClient _eventClient;
    private readonly IHealthClient _healthClient;
    private readonly IKeyValueClient _keyValueClient;
    private readonly IQueryClient _queryClient;
    private readonly ISessionClient _sessionClient;
    private readonly IStatusClient _statusClient;

    public ConsulClient(IAclClient aclClient,
        IAgentClient agentClient,
        ICatalogClient catalogClient,
        ICoordinateClient coordinateClient,
        IEventClient eventClient,
        IHealthClient healthClient,
        IKeyValueClient keyValueClient,
        IQueryClient queryClient,
        ISessionClient sessionClient,
        IStatusClient statusClient )
    {
        _aclClient = aclClient;
        _agentClient = agentClient;
        _catalogClient = catalogClient;
        _coordinateClient = coordinateClient;
        _eventClient = eventClient;
        _healthClient = healthClient;
        _keyValueClient = keyValueClient;
        _queryClient = queryClient;
        _sessionClient = sessionClient;
        _statusClient = statusClient;
    }

    // -------------------------------------------------------------------------------------------
    // ACL

    public virtual ConsulResponse<string> CreateAcl(CreateAcl newAcl, string? token)
    {
        return _aclClient.CreateAcl(newAcl, token);
    }

    public virtual ConsulResponse UpdateAcl(UpdateAcl updateAcl, string? token)
    {
        return _aclClient.UpdateAcl(updateAcl, token);
    }

    public virtual ConsulResponse DeleteAcl(string aclId, string? token)
    {
        return _aclClient.DeleteAcl(aclId, token);
    }

    public virtual ConsulResponse<Acl> GetAcl(string id)
    {
        return _aclClient.GetAcl(id);
    }

    public virtual ConsulResponse<string> CloneAcl(string aclId, string? token)
    {
        return _aclClient.CloneAcl(aclId, token);
    }

    public virtual ConsulResponse<IList<Acl>> GetAclList(string? token)
    {
        return _aclClient.GetAclList(token);
    }

    // -------------------------------------------------------------------------------------------
    // Agent

    public virtual ConsulResponse<IDictionary<string, Check>> AgentChecks => _agentClient.AgentChecks;

    public virtual ConsulResponse<IDictionary<string, Agent.Model.Service>> AgentServices => _agentClient.AgentServices;

    public virtual ConsulResponse<IList<Member>> AgentMembers => _agentClient.AgentMembers;

    public virtual ConsulResponse<Self> AgentSelf => _agentClient.AgentSelf;

    public virtual ConsulResponse<Self> GetAgentSelf(string? token)
    {
        return _agentClient.GetAgentSelf(token);
    }

    public virtual ConsulResponse AgentSetMaintenance(bool maintenanceEnabled)
    {
        return _agentClient.AgentSetMaintenance(maintenanceEnabled);
    }

    public virtual ConsulResponse AgentSetMaintenance(bool maintenanceEnabled, string? reason)
    {
        return _agentClient.AgentSetMaintenance(maintenanceEnabled, reason);
    }

    public virtual ConsulResponse AgentJoin(string address, bool wan)
    {
        return _agentClient.AgentJoin(address, wan);
    }

    public virtual ConsulResponse AgentForceLeave(string node)
    {
        return _agentClient.AgentForceLeave(node);
    }

    public virtual ConsulResponse AgentCheckRegister(NewCheck newCheck)
    {
        return _agentClient.AgentCheckRegister(newCheck);
    }

    public virtual ConsulResponse AgentCheckRegister(NewCheck newCheck, string? token)
    {
        return _agentClient.AgentCheckRegister(newCheck, token);
    }

    public virtual ConsulResponse AgentCheckDeregister(string checkId)
    {
        return _agentClient.AgentCheckDeregister(checkId);
    }

    public virtual ConsulResponse AgentCheckDeregister(string checkId, string? token)
    {
        return _agentClient.AgentCheckDeregister(checkId, token);
    }

    public virtual ConsulResponse AgentCheckPass(string checkId)
    {
        return _agentClient.AgentCheckPass(checkId);
    }

    public virtual ConsulResponse AgentCheckPass(string checkId, string? note)
    {
        return _agentClient.AgentCheckPass(checkId, note);
    }

    public virtual ConsulResponse AgentCheckPass(string checkId, string? note, string? token)
    {
        return _agentClient.AgentCheckPass(checkId, note, token);
    }

    public virtual ConsulResponse AgentCheckWarn(string checkId)
    {
        return _agentClient.AgentCheckWarn(checkId);
    }

    public virtual ConsulResponse AgentCheckWarn(string checkId, string? note)
    {
        return _agentClient.AgentCheckWarn(checkId, note);
    }

    public virtual ConsulResponse AgentCheckWarn(string checkId, string? note, string? token)
    {
        return _agentClient.AgentCheckWarn(checkId, note, token);
    }

    public virtual ConsulResponse AgentCheckFail(string checkId)
    {
        return _agentClient.AgentCheckFail(checkId);
    }

    public virtual ConsulResponse AgentCheckFail(string checkId, string? note)
    {
        return _agentClient.AgentCheckFail(checkId, note);
    }

    public virtual ConsulResponse AgentCheckFail(string checkId, string? note, string? token)
    {
        return _agentClient.AgentCheckFail(checkId, note, token);
    }

    public virtual ConsulResponse AgentServiceRegister(NewService newService)
    {
        return _agentClient.AgentServiceRegister(newService);
    }

    public virtual ConsulResponse AgentServiceRegister(NewService newService, string? token)
    {
        return _agentClient.AgentServiceRegister(newService, token);
    }

    public virtual ConsulResponse AgentServiceDeregister(string serviceId)
    {
        return _agentClient.AgentServiceDeregister(serviceId);
    }

    public virtual ConsulResponse AgentServiceDeregister(string serviceId, string? token)
    {
        return _agentClient.AgentServiceDeregister(serviceId, token);
    }

    public virtual ConsulResponse AgentServiceSetMaintenance(string serviceId, bool maintenanceEnabled)
    {
        return _agentClient.AgentServiceSetMaintenance(serviceId, maintenanceEnabled);
    }

    public virtual ConsulResponse AgentServiceSetMaintenance(string serviceId, bool maintenanceEnabled, string? reason)
    {
        return _agentClient.AgentServiceSetMaintenance(serviceId, maintenanceEnabled, reason);
    }

    public virtual ConsulResponse AgentReload()
    {
        return _agentClient.AgentReload();
    }

    // -------------------------------------------------------------------------------------------
    // Catalog

    public virtual ConsulResponse CatalogRegister(CatalogRegistration catalogRegistration)
    {
        return _catalogClient.CatalogRegister(catalogRegistration);
    }

    public virtual ConsulResponse CatalogRegister(CatalogRegistration catalogRegistration, string? token)
    {
        return _catalogClient.CatalogRegister(catalogRegistration, token);
    }

    public virtual ConsulResponse CatalogDeregister(CatalogDeregistration catalogDeregistration)
    {
        return _catalogClient.CatalogDeregister(catalogDeregistration);
    }

    public virtual ConsulResponse CatalogDeregister(CatalogDeregistration catalogDeregistration, string? token)
    {
        return _catalogClient.CatalogDeregister(catalogDeregistration, token);
    }

    public virtual ConsulResponse<IList<string>> CatalogDataCenters => _catalogClient.CatalogDataCenters;

    public virtual ConsulResponse<IList<Node>> GetCatalogNodes(CatalogNodesRequest catalogNodesRequest)
    {
        return _catalogClient.GetCatalogNodes(catalogNodesRequest);
    }

    public virtual ConsulResponse<IDictionary<string, IList<string>>> GetCatalogServices(CatalogServicesRequest catalogServicesRequest)
    {
        return _catalogClient.GetCatalogServices(catalogServicesRequest);
    }

    public virtual ConsulResponse<IList<CatalogService>> GetCatalogService(string serviceName, CatalogServiceRequest catalogServiceRequest)
    {
        return _catalogClient.GetCatalogService(serviceName, catalogServiceRequest);
    }

    public virtual ConsulResponse<CatalogNode> GetCatalogNode(string nodeName, QueryParams? queryParams)
    {
        return _catalogClient.GetCatalogNode(nodeName, queryParams);
    }

    // -------------------------------------------------------------------------------------------
    // Coordinates

    public virtual ConsulResponse<IList<Datacenter>> Datacenters => _coordinateClient.Datacenters;

    public virtual ConsulResponse<IList<Coordinate.Model.Node>> GetNodes(QueryParams? queryParams)
    {
        return _coordinateClient.GetNodes(queryParams);
    }

    // -------------------------------------------------------------------------------------------
    // Event

    public virtual ConsulResponse<Event.Model.Event> EventFire(string @event, string payload, EventParams? eventParams, QueryParams queryParams)
    {
        return _eventClient.EventFire(@event, payload, eventParams, queryParams);
    }

    public virtual ConsulResponse<IList<Event.Model.Event>> EventList(EventListRequest eventListRequest)
    {
        return _eventClient.EventList(eventListRequest);
    }

    // -------------------------------------------------------------------------------------------
    // Health

    public virtual ConsulResponse<IList<Health.Model.Check>> GetHealthChecksForNode(string nodeName, QueryParams? queryParams)
    {
        return _healthClient.GetHealthChecksForNode(nodeName, queryParams);
    }

    public virtual ConsulResponse<IList<Health.Model.Check>> GetHealthChecksForService(string serviceName, HealthChecksForServiceRequest healthChecksForServiceRequest)
    {
        return _healthClient.GetHealthChecksForService(serviceName, healthChecksForServiceRequest);
    }

    public virtual ConsulResponse<IList<HealthService>> GetHealthServices(string serviceName, HealthServicesRequest healthServicesRequest)
    {
        return _healthClient.GetHealthServices(serviceName, healthServicesRequest);
    }

    public virtual ConsulResponse<IList<Health.Model.Check>> GetHealthChecksState(QueryParams? queryParams)
    {
        return _healthClient.GetHealthChecksState(queryParams);
    }

    public virtual ConsulResponse<IList<Health.Model.Check>> GetHealthChecksState(Health.Model.CheckStatus? checkStatus, QueryParams? queryParams)
    {
        return _healthClient.GetHealthChecksState(checkStatus, queryParams);
    }

    // -------------------------------------------------------------------------------------------
    // KV

    public virtual ConsulResponse<GetValue> GetKvValue(string key)
    {
        return _keyValueClient.GetKvValue(key);
    }

    public virtual ConsulResponse<GetValue> GetKvValue(string key, string? token)
    {
        return _keyValueClient.GetKvValue(key, token);
    }

    public virtual ConsulResponse<GetValue> GetKvValue(string key, QueryParams? queryParams)
    {
        return _keyValueClient.GetKvValue(key, queryParams);
    }

    public virtual ConsulResponse<GetValue> GetKvValue(string key, string? token, QueryParams? queryParams)
    {
        return _keyValueClient.GetKvValue(key, token, queryParams);
    }

    public virtual ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key)
    {
        return _keyValueClient.GetKvBinaryValue(key);
    }

    public virtual ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key, string? token)
    {
        return _keyValueClient.GetKvBinaryValue(key, token);
    }

    public virtual ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key, QueryParams? queryParams)
    {
        return _keyValueClient.GetKvBinaryValue(key, queryParams);
    }

    public virtual ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key, string? token, QueryParams? queryParams)
    {
        return _keyValueClient.GetKvBinaryValue(key, token, queryParams);
    }

    public virtual ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix)
    {
        return _keyValueClient.GetKvValues(keyPrefix);
    }

    public virtual ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix, string? token)
    {
        return _keyValueClient.GetKvValues(keyPrefix, token);
    }

    public virtual ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix, QueryParams? queryParams)
    {
        return _keyValueClient.GetKvValues(keyPrefix, queryParams);
    }

    public virtual ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix, string? token, QueryParams? queryParams)
    {
        return _keyValueClient.GetKvValues(keyPrefix, token, queryParams);
    }

    public virtual ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix)
    {
        return _keyValueClient.GetKvBinaryValues(keyPrefix);
    }

    public virtual ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix, string? token)
    {
        return _keyValueClient.GetKvBinaryValues(keyPrefix, token);
    }

    public virtual ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix, QueryParams? queryParams)
    {
        return _keyValueClient.GetKvBinaryValues(keyPrefix, queryParams);
    }

    public virtual ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix, string? token, QueryParams? queryParams)
    {
        return _keyValueClient.GetKvBinaryValues(keyPrefix, token, queryParams);
    }

    public virtual ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix)
    {
        return _keyValueClient.getKVKeysOnly(keyPrefix);
    }

    public virtual ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix, string? separator, string? token)
    {
        return _keyValueClient.getKVKeysOnly(keyPrefix, separator, token);
    }

    public virtual ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix, QueryParams? queryParams)
    {
        return _keyValueClient.getKVKeysOnly(keyPrefix, queryParams);
    }

    public virtual ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix, string? separator, string? token, QueryParams? queryParams)
    {
        return _keyValueClient.getKVKeysOnly(keyPrefix, separator, token, queryParams);
    }

    public virtual ConsulResponse<bool?> SetKvValue(string key, string value)
    {
        return _keyValueClient.SetKvValue(key, value);
    }

    public virtual ConsulResponse<bool?> SetKvValue(string key, string value, PutParams? putParams)
    {
        return _keyValueClient.SetKvValue(key, value, putParams);
    }

    public virtual ConsulResponse<bool?> SetKvValue(string key, string value, string? token, PutParams? putParams)
    {
        return _keyValueClient.SetKvValue(key, value, token, putParams);
    }

    public virtual ConsulResponse<bool?> SetKvValue(string key, string value, QueryParams? queryParams)
    {
        return _keyValueClient.SetKvValue(key, value, queryParams);
    }

    public virtual ConsulResponse<bool?> SetKvValue(string key, string value, PutParams? putParams, QueryParams? queryParams)
    {
        return _keyValueClient.SetKvValue(key, value, putParams, queryParams);
    }

    public virtual ConsulResponse<bool?> SetKvValue(string key, string value, string? token, PutParams? putParams, QueryParams? queryParams)
    {
        return _keyValueClient.SetKvValue(key, value, token, putParams, queryParams);
    }

    public virtual ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value)
    {
        return _keyValueClient.SetKvBinaryValue(key, value);
    }

    public virtual ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, PutParams? putParams)
    {
        return _keyValueClient.SetKvBinaryValue(key, value, putParams);
    }

    public virtual ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, string? token, PutParams? putParams)
    {
        return _keyValueClient.SetKvBinaryValue(key, value, token, putParams);
    }

    public virtual ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, QueryParams? queryParams)
    {
        return _keyValueClient.SetKvBinaryValue(key, value, queryParams);
    }

    public virtual ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, PutParams? putParams, QueryParams? queryParams)
    {
        return _keyValueClient.SetKvBinaryValue(key, value, putParams, queryParams);
    }

    public virtual ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, string? token, PutParams? putParams, QueryParams? queryParams)
    {
        return _keyValueClient.SetKvBinaryValue(key, value, token, putParams, queryParams);
    }

    public virtual ConsulResponse DeleteKvValue(string key)
    {
        return _keyValueClient.DeleteKvValue(key);
    }

    public virtual ConsulResponse DeleteKvValue(string key, string? token)
    {
        return _keyValueClient.DeleteKvValue(key, token);
    }

    public virtual ConsulResponse DeleteKvValue(string key, QueryParams? queryParams)
    {
        return _keyValueClient.DeleteKvValue(key, queryParams);
    }

    public virtual ConsulResponse DeleteKvValue(string key, string? token, QueryParams? queryParams)
    {
        return _keyValueClient.DeleteKvValue(key, token, queryParams);
    }

    public virtual ConsulResponse DeleteKvValues(string key)
    {
        return _keyValueClient.DeleteKvValues(key);
    }

    public virtual ConsulResponse DeleteKvValues(string key, string? token)
    {
        return _keyValueClient.DeleteKvValues(key, token);
    }

    public virtual ConsulResponse DeleteKvValues(string key, QueryParams? queryParams)
    {
        return _keyValueClient.DeleteKvValues(key, queryParams);
    }

    public virtual ConsulResponse DeleteKvValues(string key, string? token, QueryParams? queryParams)
    {
        return _keyValueClient.DeleteKvValues(key, token, queryParams);
    }

    // -------------------------------------------------------------------------------------------
    // Prepared Query

    public virtual ConsulResponse<QueryExecution> ExecutePreparedQuery(string uuid, QueryParams? queryParams)
    {
        return _queryClient.ExecutePreparedQuery(uuid, queryParams);
    }

    // -------------------------------------------------------------------------------------------
    // Session

    public virtual ConsulResponse<string> SessionCreate(NewSession newSession, QueryParams? queryParams)
    {
        return _sessionClient.SessionCreate(newSession, queryParams);
    }

    public virtual ConsulResponse<string> SessionCreate(NewSession newSession, QueryParams? queryParams, string? token)
    {
        return _sessionClient.SessionCreate(newSession, queryParams, token);
    }

    public virtual ConsulResponse SessionDestroy(string session, QueryParams? queryParams)
    {
        return _sessionClient.SessionDestroy(session, queryParams);
    }

    public virtual ConsulResponse SessionDestroy(string session, QueryParams? queryParams, string? token)
    {
        return _sessionClient.SessionDestroy(session, queryParams, token);
    }

    public virtual ConsulResponse<Session.Model.Session> GetSessionInfo(string session, QueryParams? queryParams)
    {
        return _sessionClient.GetSessionInfo(session, queryParams);
    }

    public virtual ConsulResponse<Session.Model.Session> GetSessionInfo(string session, QueryParams? queryParams, string? token)
    {
        return _sessionClient.GetSessionInfo(session, queryParams, token);
    }

    public virtual ConsulResponse<IList<Session.Model.Session>> GetSessionNode(string node, QueryParams? queryParams)
    {
        return _sessionClient.GetSessionNode(node, queryParams);
    }

    public virtual ConsulResponse<IList<Session.Model.Session>> GetSessionNode(string node, QueryParams? queryParams, string? token)
    {
        return _sessionClient.GetSessionNode(node, queryParams, token);
    }

    public virtual ConsulResponse<IList<Session.Model.Session>> GetSessionList(QueryParams? queryParams)
    {
        return _sessionClient.GetSessionList(queryParams);
    }

    public virtual ConsulResponse<IList<Session.Model.Session>> GetSessionList(QueryParams? queryParams, string? token)
    {
        return _sessionClient.GetSessionList(queryParams, token);
    }

    public virtual ConsulResponse<Session.Model.Session> RenewSession(string session, QueryParams? queryParams)
    {
        return _sessionClient.RenewSession(session, queryParams);
    }

    public virtual ConsulResponse<Session.Model.Session> RenewSession(string session, QueryParams? queryParams, string? token)
    {
        return _sessionClient.RenewSession(session, queryParams, token);
    }

    // -------------------------------------------------------------------------------------------
    // Status

    public virtual ConsulResponse<string> StatusLeader => _statusClient.StatusLeader;

    public virtual ConsulResponse<IList<string>> StatusPeers => _statusClient.StatusPeers;
}