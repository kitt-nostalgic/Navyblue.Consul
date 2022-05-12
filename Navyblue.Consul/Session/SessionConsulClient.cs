using Navyblue.BaseLibrary;
using Navyblue.Consul.Session.Model;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul.Session;

/// <summary>
///
/// </summary>
public sealed class SessionConsulClient : ISessionClient
{
    private readonly IConsulRawClient _consulRawClient;

    public SessionConsulClient(IConsulRawClient consulRawClient)
    {
        this._consulRawClient = consulRawClient;
    }

    public ConsulResponse<string> SessionCreate(NewSession newSession, QueryParams? queryParams)
    {
        return SessionCreate(newSession, queryParams, null);
    }

    public ConsulResponse<string> SessionCreate(NewSession newSession, QueryParams? queryParams, string? token)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        string json = newSession.ToJson();
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/session/create", json, queryParams, tokenParam);

        if (httpResponse.StatusCode == 200)
        {
            IDictionary<string, string> value = httpResponse.Content.FromJson<Dictionary<string, string>>();
            return new ConsulResponse<string>(value["ID"], httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse SessionDestroy(string session, QueryParams? queryParams)
    {
        return SessionDestroy(session, queryParams, null);
    }

    public ConsulResponse SessionDestroy(string session, QueryParams? queryParams, string? token)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/session/destroy/" + session, "", queryParams, tokenParam);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<Model.Session> GetSessionInfo(string session, QueryParams? queryParams)
    {
        return GetSessionInfo(session, queryParams, null);
    }

    public ConsulResponse<Model.Session> GetSessionInfo(string session, QueryParams? queryParams, string? token)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/session/info/" + session, queryParams, tokenParam);

        if (httpResponse.StatusCode == 200)
        {
            IList<Model.Session> value = httpResponse.Content.FromJson<List<Model.Session>>();

            if (value == null || value.Count == 0)
            {
                return new ConsulResponse<Model.Session>(null, httpResponse);
            }

            if (value.Count == 1)
            {
                return new ConsulResponse<Model.Session>(value[0], httpResponse);
            }
            throw new ConsulException("Strange response (list size=" + value.Count + ")");
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<Model.Session>> GetSessionNode(string node, QueryParams? queryParams)
    {
        return GetSessionNode(node, queryParams, null);
    }

    public ConsulResponse<IList<Model.Session>> GetSessionNode(string node, QueryParams? queryParams, string? token)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/session/node/" + node, queryParams, tokenParam);

        if (httpResponse.StatusCode == 200)
        {
            IList<Model.Session> value = httpResponse.Content.FromJson<List<Model.Session>>();
            return new ConsulResponse<IList<Model.Session>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<Model.Session>> GetSessionList(QueryParams? queryParams)
    {
        return GetSessionList(queryParams, null);
    }

    public ConsulResponse<IList<Model.Session>> GetSessionList(QueryParams? queryParams, string? token)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/session/list", queryParams, tokenParam);

        if (httpResponse.StatusCode == 200)
        {
            IList<Model.Session> value = httpResponse.Content.FromJson<List<Model.Session>>();
            return new ConsulResponse<IList<Model.Session>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<Model.Session> RenewSession(string session, QueryParams? queryParams)
    {
        return RenewSession(session, queryParams, null);
    }

    public ConsulResponse<Model.Session> RenewSession(string session, QueryParams? queryParams, string? token)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/session/renew/" + session, "", queryParams, tokenParam);

        if (httpResponse.StatusCode == 200)
        {
            IList<Model.Session> value = httpResponse.Content.FromJson<List<Model.Session>>();

            if (value.Count == 1)
            {
                return new ConsulResponse<Model.Session>(value[0], httpResponse);
            }

            throw new ConsulException("Strange response (list size=" + value.Count + ")");
        }

        throw new OperationException(httpResponse);
    }
}