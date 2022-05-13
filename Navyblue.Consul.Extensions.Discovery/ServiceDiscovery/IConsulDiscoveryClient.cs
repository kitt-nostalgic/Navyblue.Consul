using Navyblue.Consul.Health.Model;

namespace Navyblue.Consul.Extensions.Discovery.ServiceDiscovery;

/// <summary>
/// 
/// </summary>
public interface IConsulDiscoveryClient
{
    /// <summary>
    /// Gets the instances.
    /// </summary>
    /// <param name="serviceId">The service identifier.</param>
    /// <returns></returns>
    List<Service> GetInstances(string serviceId);

    /// <summary>
    /// Gets the instances.
    /// </summary>
    /// <param name="serviceId">The service identifier.</param>
    /// <param name="queryParams">The query parameters.</param>
    /// <returns></returns>
    List<Service> GetInstances(string serviceId, QueryParams queryParams);

    /// <summary>
    /// Gets all instances.
    /// </summary>
    /// <returns></returns>
    List<Service> GetAllInstances();

    /// <summary>
    /// Gets the services.
    /// </summary>
    /// <returns></returns>
    List<string>? GetServices();

    /// <summary>
    /// Gets the order.
    /// </summary>
    /// <returns></returns>
    int GetOrder();
}