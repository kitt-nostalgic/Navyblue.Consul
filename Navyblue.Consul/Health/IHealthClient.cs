using Navyblue.Consul.Health.Model;

namespace Navyblue.Consul.Health;

/// <summary>
///
/// </summary>
public interface IHealthClient
{
    ConsulResponse<IList<Check>> GetHealthChecksForNode(string nodeName, QueryParams? queryParams);

    ConsulResponse<IList<Check>> GetHealthChecksForService(string serviceName, HealthChecksForServiceRequest healthChecksForServiceRequest);

    ConsulResponse<IList<HealthService>> GetHealthServices(string serviceName, HealthServicesRequest healthServicesRequest);

    ConsulResponse<IList<Check>> GetHealthChecksState(QueryParams? queryParams);

    ConsulResponse<IList<Check>> GetHealthChecksState(CheckStatus? checkStatus, QueryParams? queryParams);
}