using Navyblue.Consul.Transport;

namespace Navyblue.Consul;


public interface IConsulRawClient
{
    ConsulHttpResponse MakeGetRequest(string endpoint, params IUrlParameters?[]? urlParams);

    ConsulHttpResponse MakeGetRequest(string endpoint, IList<IUrlParameters?>? urlParams);

    ConsulHttpResponse MakeGetRequest(Request request);

    ConsulHttpResponse MakePutRequest(string endpoint, string content, params IUrlParameters?[]? urlParams);

    ConsulHttpResponse MakePutRequest(Request request);

    ConsulHttpResponse MakeDeleteRequest(Request request);
}
