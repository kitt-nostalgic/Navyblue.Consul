using WebApiClientCore;

namespace Navyblue.Consul.Extensions.WebApiClient
{
    public class ConsulHttpApiOptions : HttpApiOptions
    {
        /// <summary>
        /// Get a service name which has been existed in Consul
        /// Note: The HttpHost and HttpHostAttribute will be inactive after you set value to ServiceName
        /// </summary>
        public string ConsulServiceName { get; set; }
    }
}
