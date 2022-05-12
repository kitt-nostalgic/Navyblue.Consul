using Navyblue.Consul.Session.Model;

namespace Navyblue.Consul.Session;

/// <summary>
///
/// </summary>
public interface ISessionClient
{
    ConsulResponse<string> SessionCreate(NewSession newSession, QueryParams? queryParams);

    ConsulResponse<string> SessionCreate(NewSession newSession, QueryParams? queryParams, string? token);

    ConsulResponse SessionDestroy(string session, QueryParams? queryParams);

    ConsulResponse SessionDestroy(string session, QueryParams? queryParams, string? token);

    ConsulResponse<Model.Session> GetSessionInfo(string session, QueryParams? queryParams);

    ConsulResponse<Model.Session> GetSessionInfo(string session, QueryParams? queryParams, string? token);

    ConsulResponse<IList<Model.Session>> GetSessionNode(string node, QueryParams? queryParams);

    ConsulResponse<IList<Model.Session>> GetSessionNode(string node, QueryParams? queryParams, string? token);

    ConsulResponse<IList<Model.Session>> GetSessionList(QueryParams? queryParams);

    ConsulResponse<IList<Model.Session>> GetSessionList(QueryParams? queryParams, string? token);

    ConsulResponse<Model.Session> RenewSession(string session, QueryParams? queryParams);

    ConsulResponse<Model.Session> RenewSession(string session, QueryParams? queryParams, string? token);
}