namespace Navyblue.Consul.Extensions.WebApiClient;

public class ConsulServiceAttribute : Attribute
{
    public string ServiceName { get; set; }

    public ConsulServiceAttribute(string serviceName)
    {
        ServiceName = serviceName;
    }
}