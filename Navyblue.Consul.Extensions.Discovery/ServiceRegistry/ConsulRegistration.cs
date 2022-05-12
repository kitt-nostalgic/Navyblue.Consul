using Microsoft.Extensions.Options;
using Navyblue.Consul.Agent.Model;

namespace Navyblue.Consul.Extensions.Discovery.ServiceRegistry;

public abstract class ConsulRegistration
{
    public NewService Service { get;}

    protected readonly ConsulDiscoveryConfiguration _consulDiscoveryConfiguration;

    public ConsulRegistration(NewService service,
        ConsulDiscoveryConfiguration consulDiscoveryConfiguration)
    {
        this.Service = service;
        this._consulDiscoveryConfiguration = consulDiscoveryConfiguration;
    }

    public string? GetInstanceId()
    {
        return Service.Id;
    }

    public string? GetServiceId()
    {
        return Service.Name;
    }

    public string? GetHost()
    {
        return Service.Address;
    }

    public int GetPort()
    {
        return Service.Port.GetValueOrDefault();
    }

    public bool IsSecure()
    {
        return this._consulDiscoveryConfiguration.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
    }

    public Dictionary<string, string> GetMetadata()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        if (Service.Tags == null) return dictionary;

        foreach (var serviceTag in Service.Tags)
        {
            string[] splitTags = serviceTag.Split("=");
            switch (serviceTag.Length)
            {
                case 0:
                    break;
                case 1:
                    dictionary.Add(splitTags[0], splitTags[0]);
                    break;
                case 2:
                    dictionary.Add(splitTags[0], splitTags[1]);
                    break;
            }


            dictionary.Add(splitTags[0],splitTags[1]);
        }

        return dictionary;
    }

    public Uri GetUri()
    {
        string scheme = IsSecure() ? "https" : "http";
        int port = GetPort();
        if (port <= 0)
        {
            port = (IsSecure()) ? 443 : 80;
        }

        string uri = string.Format("%s://%s:%s", scheme, GetHost(), port);

        return new Uri(uri);
    }

}