namespace Navyblue.Consul.Extensions.Discovery.ServiceRegistry;

public interface IConsulServiceRegistryClient
{
    void Register();

    void Deregister();

    string? GetAppName();

    void SetStatus(string status);

    string GetStatus();
}