namespace Navyblue.Consul.Status;

/// <summary>
///
/// </summary>
public interface IStatusClient
{
    ConsulResponse<string> StatusLeader { get; }

    ConsulResponse<IList<string>> StatusPeers { get; }
}