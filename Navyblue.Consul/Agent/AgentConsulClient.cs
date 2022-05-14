using Navyblue.BaseLibrary;
using Navyblue.Consul.Agent.Model;
using Navyblue.Consul.Agent.Model.SelfModel;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul.Agent;

/// <summary>
///
/// </summary>
public sealed class AgentConsulClient : IAgentClient
{
    private readonly IConsulRawClient _consulRawClient;

    public AgentConsulClient(IConsulRawClient consulRawClient)
    {
        _consulRawClient = consulRawClient;
    }

    public ConsulResponse<IDictionary<string, Check>> AgentChecks
    {
        get
        {
            ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/agent/checks");

            if (httpResponse.StatusCode == 200)
            {
                IDictionary<string, Check> value = httpResponse.Content.FromJson<Dictionary<string, Check>>();
                return new ConsulResponse<IDictionary<string, Check>>(value, httpResponse);
            }

            throw new OperationException(httpResponse);
        }
    }

    public ConsulResponse<IDictionary<string, Service>> AgentServices
    {
        get
        {
            ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/agent/services");

            if (httpResponse.StatusCode == 200)
            {
                IDictionary<string, Service> agentServices = httpResponse.Content.FromJson<Dictionary<string, Service>>();
                return new ConsulResponse<IDictionary<string, Service>>(agentServices, httpResponse);
            }

            throw new OperationException(httpResponse);
        }
    }

    public ConsulResponse<IList<Member>> AgentMembers
    {
        get
        {
            ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/agent/members");

            if (httpResponse.StatusCode == 200)
            {
                IList<Member> members = httpResponse.Content.FromJson<List<Member>>();
                return new ConsulResponse<IList<Member>>(members, httpResponse);
            }

            throw new OperationException(httpResponse);
        }
    }

    public ConsulResponse<Self> AgentSelf
    {
        get
        {
            return GetAgentSelf(null);
        }
    }

    public ConsulResponse<Self> GetAgentSelf(string? token)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/agent/self", tokenParam!);

        if (httpResponse.StatusCode == 200)
        {
            Self self = httpResponse.Content.FromJson<Self>();
            return new ConsulResponse<Self>(self, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentSetMaintenance(bool maintenanceEnabled)
    {
        return AgentSetMaintenance(maintenanceEnabled, null);
    }

    public ConsulResponse AgentSetMaintenance(bool maintenanceEnabled, string? reason)
    {
        IUrlParameters? maintenanceParameter = new SingleUrlParameters("enable", Convert.ToString(maintenanceEnabled));
        IUrlParameters? reasonParameter = !string.IsNullOrWhiteSpace(reason) ? new SingleUrlParameters("reason", reason) : null;

        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/maintenance", "", maintenanceParameter, reasonParameter!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentJoin(string address, bool wan)
    {
        IUrlParameters? wanParams = wan ? new SingleUrlParameters("wan", "1") : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/join/" + address, "", wanParams!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentForceLeave(string node)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/force-leave/" + node, "");

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentCheckRegister(NewCheck newCheck)
    {
        return AgentCheckRegister(newCheck, null);
    }

    public ConsulResponse AgentCheckRegister(NewCheck newCheck, string? token)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        string json = newCheck.ToJson();
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/check/register", json, tokenParam!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentCheckDeregister(string checkId)
    {
        return AgentCheckDeregister(checkId, null);
    }

    public ConsulResponse AgentCheckDeregister(string checkId, string? token)
    {
        IUrlParameters? tokenParameter = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/check/deregister/" + checkId, "", tokenParameter!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentCheckPass(string checkId)
    {
        return AgentCheckPass(checkId, null);
    }

    public ConsulResponse AgentCheckPass(string checkId, string? note)
    {
        return AgentCheckPass(checkId, note, null);
    }

    public ConsulResponse AgentCheckPass(string checkId, string? note, string? token)
    {
        IUrlParameters? noteParameter = !string.IsNullOrWhiteSpace(note) ? new SingleUrlParameters("note", note) : null;
        IUrlParameters? tokenParameter = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/check/pass/" + checkId, "", noteParameter!, tokenParameter!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentCheckWarn(string checkId)
    {
        return AgentCheckWarn(checkId, null);
    }

    public ConsulResponse AgentCheckWarn(string checkId, string? note)
    {
        return AgentCheckWarn(checkId, note, null);
    }

    public ConsulResponse AgentCheckWarn(string checkId, string? note, string? token)
    {
        IUrlParameters? noteParameter = !string.IsNullOrWhiteSpace(note) ? new SingleUrlParameters("note", note) : null;
        IUrlParameters? tokenParameter = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/check/warn/" + checkId, "", noteParameter!, tokenParameter!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentCheckFail(string checkId)
    {
        return AgentCheckFail(checkId, null);
    }

    public ConsulResponse AgentCheckFail(string checkId, string? note)
    {
        return AgentCheckFail(checkId, note, null);
    }

    public ConsulResponse AgentCheckFail(string checkId, string? note, string? token)
    {
        IUrlParameters? noteParameter = !string.IsNullOrWhiteSpace(note) ? new SingleUrlParameters("note", note) : null;
        IUrlParameters? tokenParameter = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/check/fail/" + checkId, "", noteParameter!, tokenParameter!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentServiceRegister(NewService newService)
    {
        return AgentServiceRegister(newService, null);
    }

    public ConsulResponse AgentServiceRegister(NewService newService, string? token)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        string json = newService.ToJson();
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/service/register", json, tokenParam!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentServiceDeregister(string serviceId)
    {
        return AgentServiceDeregister(serviceId, null);
    }

    public ConsulResponse AgentServiceDeregister(string serviceId, string? token)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/service/deregister/" + serviceId, "", tokenParam!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentServiceSetMaintenance(string serviceId, bool maintenanceEnabled)
    {
        return AgentServiceSetMaintenance(serviceId, maintenanceEnabled, null);
    }

    public ConsulResponse AgentServiceSetMaintenance(string serviceId, bool maintenanceEnabled, string? reason)
    {
        IUrlParameters? maintenanceParameter = new SingleUrlParameters("enable", Convert.ToString(maintenanceEnabled));
        IUrlParameters? reasonParameter = !string.IsNullOrWhiteSpace(reason) ? new SingleUrlParameters("reason", reason) : null;

        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/service/maintenance/" + serviceId, "", maintenanceParameter, reasonParameter);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse AgentReload()
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/agent/reload", "");

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }
}