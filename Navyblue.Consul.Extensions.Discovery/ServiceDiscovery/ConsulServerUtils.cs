using System.Net;
using Navyblue.Consul.Health.Model;
using Serilog;

namespace Navyblue.Consul.Extensions.Discovery.ServiceDiscovery;

public class ConsulServerUtils
{
    public static string? FindHost(HealthService healthService)
    {
        Service? service = healthService.Service;
        Node? node = healthService.Node;

        if (service != null && !string.IsNullOrWhiteSpace(service.Address))
        {
            return FixIPv6Address(service.Address);
        }

        if (node != null && !string.IsNullOrWhiteSpace(node.Address))
        {
            return FixIPv6Address(node.Address);
        }

        return node?.NodeText;
    }

    public static string FixIPv6Address(string address)
    {
        IPHostEntry entry = Dns.GetHostEntry(address);

        return entry.HostName;
    }
}