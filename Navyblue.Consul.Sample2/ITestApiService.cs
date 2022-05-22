using Navyblue.Consul.Extensions.WebApiClient;
using WebApiClientCore.Attributes;

namespace Navyblue.Consul.Sample2;

[ConsulService("NavyBlue.WebTest")]
public interface ITestApiService
{
    [HttpGet("api/consul/InvokeTest")]
    Task<string> GetInvokeTestResult();
}