using Microsoft.Extensions.Options;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul;

/// <summary>
///
/// </summary>
public class ConsulRawClient : IConsulRawClient
{
    private readonly IHttpTransport _httpTransport;
    private readonly string _agentAddress;

    public ConsulRawClient(IOptions<ConsulConfiguration> consulConfigurationOptions,
        IHttpTransport httpTransport)
    {
        var consulConfiguration = consulConfigurationOptions.Value;
        _httpTransport = httpTransport;

        _agentAddress = Utils.AssembleAgentAddress(consulConfiguration.Host, consulConfiguration.Port, consulConfiguration.PrefixPath);
    }

    public ConsulHttpResponse MakeGetRequest(string endpoint, params IUrlParameters?[]? urlParams)
    {
        return MakeGetRequest(endpoint, urlParams?.ToList());
    }

    public ConsulHttpResponse MakeGetRequest(string endpoint, IList<IUrlParameters?>? urlParams)
    {
        urlParams?.Remove(null);
        string url = PrepareUrl(_agentAddress + endpoint);
        url = Utils.GenerateUrl(url, urlParams);

        ConsulHttpRequest request = new ConsulHttpRequest
        {
            Url = url
        };

        return _httpTransport.Get(request);
    }

    public ConsulHttpResponse MakeGetRequest(Request request)
    {
        string url = PrepareUrl(_agentAddress + request.Endpoint);
        url = Utils.GenerateUrl(url, request.UrlParameters);

        ConsulHttpRequest httpRequest = new ConsulHttpRequest
        {
            Url = url,
            Headers = Utils.CreateTokenMap(request.Token)
        };

        return _httpTransport.Get(httpRequest);
    }

    public ConsulHttpResponse MakePutRequest(string endpoint, string content, params IUrlParameters?[]? urlParams)
    {
        string url = PrepareUrl(_agentAddress + endpoint);
        url = Utils.GenerateUrl(url, urlParams);

        ConsulHttpRequest request = new ConsulHttpRequest
        {
            Url = url,
            Content = content
        };

        return _httpTransport.Put(request);
    }

    public ConsulHttpResponse MakePutRequest(Request request)
    {
        string url = PrepareUrl(_agentAddress + request.Endpoint);
        url = Utils.GenerateUrl(url, request.UrlParameters);

        ConsulHttpRequest httpRequest = new ConsulHttpRequest
        {
            Url = url,
            BinaryContent = request.BinaryContent,
            Headers = Utils.CreateTokenMap(request.Token)
        };

        return _httpTransport.Put(httpRequest);
    }

    public ConsulHttpResponse MakeDeleteRequest(Request request)
    {
        string url = PrepareUrl(_agentAddress + request.Endpoint);
        url = Utils.GenerateUrl(url, request.UrlParameters);

        ConsulHttpRequest httpRequest = new ConsulHttpRequest
        {
            Url = url,
            Headers = Utils.CreateTokenMap(request.Token)
        };

        return _httpTransport.Delete(httpRequest);
    }

    private string PrepareUrl(string url)
    {
        if (url.Contains(" "))
        {
            return Utils.EncodeUrl(url);
        }

        return url;
    }
}