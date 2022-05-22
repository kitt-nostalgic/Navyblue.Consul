using Navyblue.Consul.Extensions.WebApiClient;
using WebApiClientCore.Attributes;

namespace Navyblue.Consul.Sample;

[ConsulService("NavyBlue.WebTest")]
public interface ITestApiService
{
    [HttpGet("api/consul/health")]
    Task<string> GetHealthResult();
}