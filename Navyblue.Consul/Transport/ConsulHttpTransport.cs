using System.Text;
using Serilog;
using Serilog.Core;

namespace Navyblue.Consul.Transport;

public class ConsulHttpTransport : IHttpTransport
{
    private readonly Logger _log = new LoggerConfiguration().WriteTo.Console().WriteTo.File("log.txt").CreateLogger();

    internal const int DefaultMaxConnections = 1000;
    private const int DefaultMaxPerRouteConnections = 500;
    private const int DefaultConnectionTimeout = 10 * 1000; // 10 sec

    // 10 minutes for read timeout due to blocking queries timeout
    // https://www.consul.io/api/index.html#blocking-queries
    private const int DefaultReadTimeout = 1000 * 60 * 10; // 10 min

    public virtual ConsulHttpResponse Get(ConsulHttpRequest request)
    {
        HttpRequestMessage httpGet = new HttpRequestMessage(HttpMethod.Get, request.Url);
        AddHeadersToRequest(httpGet, request.Headers);

        return ExecuteRequest(httpGet);
    }

    public virtual ConsulHttpResponse Put(ConsulHttpRequest request)
    {
        HttpRequestMessage httpPut = new HttpRequestMessage(HttpMethod.Put, request.Url);
        AddHeadersToRequest(httpPut, request.Headers);
        httpPut.Content = !string.IsNullOrWhiteSpace(request.Content) ? 
            new StringContent(request.Content, Encoding.UTF8) : 
            new ByteArrayContent((byte[])((Array)request.BinaryContent!));

        return ExecuteRequest(httpPut);
    }

    public virtual ConsulHttpResponse Delete(ConsulHttpRequest request)
    {
        HttpRequestMessage httpDelete = new HttpRequestMessage(HttpMethod.Delete, request.Url);
        AddHeadersToRequest(httpDelete, request.Headers);
        return ExecuteRequest(httpDelete);
    }

    /// <summary>
    /// You should override this method to instantiate ready to use HttpClient
    /// </summary>
    /// <returns> HttpClient </returns>

    private ConsulHttpResponse ExecuteRequest(HttpRequestMessage httpRequest)
    {
        LogRequest(httpRequest);

        try
        {
            SocketsHttpHandler socketsHttpHandler = new SocketsHttpHandler
            {
                MaxConnectionsPerServer = DefaultMaxPerRouteConnections,
                ConnectTimeout = TimeSpan.FromSeconds(DefaultConnectionTimeout),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(DefaultReadTimeout)
            };

            HttpClient httpClient = new HttpClient(socketsHttpHandler);
            var response = httpClient.Send(httpRequest);

            int statusCode = (int)response.StatusCode;
            string? statusMessage = response.ReasonPhrase;
            string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            long? consulIndex = ParseUnsignedLong(response.Headers.Contains("X-Consul-Index") ? response.Headers.GetValues("X-Consul-Index").FirstOrDefault() : null);
            bool? consulKnownLeader = ParseBoolean(response.Headers.Contains("X-Consul-Knownleader") ? response.Headers.GetValues("X-Consul-Knownleader").FirstOrDefault() : null);
            long? consulLastContact = ParseUnsignedLong(response.Headers.Contains("X-Consul-Lastcontact") ? response.Headers.GetValues("X-Consul-Lastcontact").FirstOrDefault() : null);
            
            return new ConsulHttpResponse
            {
                StatusCode = statusCode, 
                StatusMessage = statusMessage,
                Content = content,
                ConsulIndex = consulIndex, 
                ConsulKnownLeader = consulKnownLeader, 
                ConsulLastContact = consulLastContact
            };
        }
        catch (IOException e)
        {
            throw new TransportException(e);
        }
    }

    private long? ParseUnsignedLong(string? headerValue)
    {
        if (string.IsNullOrWhiteSpace(headerValue))
        {
            return null;
        }

        try
        {
            return long.Parse(headerValue);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private bool? ParseBoolean(string? header)
    {
        bool.TryParse(header, out bool result);

        return result;
    }

    private void AddHeadersToRequest(HttpRequestMessage request, IDictionary<string, string?>? headers)
    {
        if (headers == null) return;

        foreach (KeyValuePair<string, string?> headerValue in headers)
        {
            string name = headerValue.Key;
            string? value = headerValue.Value;

            if (!request.Headers.Contains(name))
            {
                request.Headers.Add(name, value);
            }
        }
    }

    private void LogRequest(HttpRequestMessage httpRequest)
    {
        StringBuilder sb = new StringBuilder();

        // method
        sb.Append(httpRequest.Method);
        sb.Append(" ");

        // url
        sb.Append(httpRequest.RequestUri);
        sb.Append(" ");

        // headers, if any
        var iterator = httpRequest.Headers;
        if (iterator.Any())
        {
            sb.Append("Headers:[");

            foreach (var keyValue in iterator)
            {
                sb.Append(keyValue.Key).Append("=").Append(keyValue.Value);
                sb.Append(";");
            }

            sb.Append("] ");
        }

        _log.Information(sb.ToString());
    }
}