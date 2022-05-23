using Navyblue.BaseLibrary;
using Navyblue.Consul.Health.Model;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul.Health;

/// <summary>
///
/// </summary>
public sealed class HealthConsulClient : IHealthClient
{
    private readonly IConsulRawClient _consulRawClient;

    public HealthConsulClient(IConsulRawClient consulRawClient)
    {
        _consulRawClient = consulRawClient;
    }

    public ConsulResponse<IList<Check>> GetHealthChecksForNode(string nodeName, QueryParams? queryParams)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/health/node/" + nodeName, queryParams);

        if (httpResponse.StatusCode == 200)
        {
            IList<Check> value = httpResponse.Content.FromJson<List<Check>>();
            return new ConsulResponse<IList<Check>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<Check>> GetHealthChecksForService(string serviceName, HealthChecksForServiceRequest healthChecksForServiceRequest)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/health/checks/" + serviceName, healthChecksForServiceRequest.AsUrlParameters());

        if (httpResponse.StatusCode == 200)
        {
            IList<Check> value = httpResponse.Content.FromJson<List<Check>>();
            return new ConsulResponse<IList<Check>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<HealthService>> GetHealthServices(string serviceName, HealthServicesRequest healthServicesRequest)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/health/service/" + serviceName, healthServicesRequest.AsUrlParameters());

        if (httpResponse.StatusCode == 200)
        {
            IList<HealthService> value = httpResponse.Content.FromJson<List<HealthService>>();
            return new ConsulResponse<IList<HealthService>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<Check>> GetHealthChecksState(QueryParams? queryParams)
    {
        return GetHealthChecksState(null, queryParams);
    }

    public ConsulResponse<IList<Check>> GetHealthChecksState(CheckStatus? checkStatus, QueryParams? queryParams)
    {
        string? status = checkStatus.ToString()?.ToLower();//TODO
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/health/state/" + status, queryParams);

        if (httpResponse.StatusCode == 200)
        {
            IList<Check> value = httpResponse.Content.FromJson<List<Check>>();
            return new ConsulResponse<IList<Check>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }
}