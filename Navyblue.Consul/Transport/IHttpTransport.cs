namespace Navyblue.Consul.Transport;

/// <summary>
///
/// </summary>
public interface IHttpTransport
{
    ConsulHttpResponse Get(ConsulHttpRequest request);

    ConsulHttpResponse Put(ConsulHttpRequest request);

    ConsulHttpResponse Delete(ConsulHttpRequest request);
}