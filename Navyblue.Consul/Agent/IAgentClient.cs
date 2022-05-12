using Navyblue.Consul.Agent.Model;
using Navyblue.Consul.Agent.Model.SelfModel;

namespace Navyblue.Consul.Agent;

/// <summary>
///
/// </summary>
public interface IAgentClient
{
    ConsulResponse<IDictionary<string, Check>> AgentChecks { get; }

    ConsulResponse<IDictionary<string, Service>> AgentServices { get; }

    ConsulResponse<IList<Member>> AgentMembers { get; }

    ConsulResponse<Self> AgentSelf { get; }

    ConsulResponse<Self> GetAgentSelf(string? token);

    ConsulResponse AgentSetMaintenance(bool maintenanceEnabled);

    ConsulResponse AgentSetMaintenance(bool maintenanceEnabled, string? reason);

    ConsulResponse AgentJoin(string address, bool wan);

    ConsulResponse AgentForceLeave(string node);

    ConsulResponse AgentCheckRegister(NewCheck newCheck);

    ConsulResponse AgentCheckRegister(NewCheck newCheck, string? token);

    ConsulResponse AgentCheckDeregister(string checkId);

    ConsulResponse AgentCheckDeregister(string checkId, string? token);

    ConsulResponse AgentCheckPass(string checkId);

    ConsulResponse AgentCheckPass(string checkId, string? note);

    ConsulResponse AgentCheckPass(string checkId, string? note, string? token);

    ConsulResponse AgentCheckWarn(string checkId);

    ConsulResponse AgentCheckWarn(string checkId, string? note);

    ConsulResponse AgentCheckWarn(string checkId, string? note, string? token);

    ConsulResponse AgentCheckFail(string checkId);

    ConsulResponse AgentCheckFail(string checkId, string? note);

    ConsulResponse AgentCheckFail(string checkId, string? note, string? token);

    ConsulResponse AgentServiceRegister(NewService newService);

    ConsulResponse AgentServiceRegister(NewService newService, string? token);

    ConsulResponse AgentServiceDeregister(string serviceId);

    ConsulResponse AgentServiceDeregister(string serviceId, string? token);

    ConsulResponse AgentServiceSetMaintenance(string serviceId, bool maintenanceEnabled);

    ConsulResponse AgentServiceSetMaintenance(string serviceId, bool maintenanceEnabled, string? reason);

    ConsulResponse AgentReload();
}