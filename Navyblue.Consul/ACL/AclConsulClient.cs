using Navyblue.BaseLibrary;
using Navyblue.Consul.ACL.Model;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul.ACL;

/// <summary>
///
/// </summary>
public sealed class AclConsulClient : IAclClient
{
    private readonly IConsulRawClient _consulRawClient;

    public AclConsulClient(IConsulRawClient consulRawClient)
    {
        _consulRawClient = consulRawClient;
    }

    public ConsulResponse<string> CreateAcl(CreateAcl newAcl, string? token)
    {
        IUrlParameters? tokenParams = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        string json = newAcl.ToJson();
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/acl/create", json, tokenParams!);

        if (httpResponse.StatusCode == 200)
        {
            IDictionary<string, string> value = httpResponse.Content.FromJson<IDictionary<string, string>>();
            return new ConsulResponse<string>(value["ID"], httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse UpdateAcl(UpdateAcl updateAcl, string? token)
    {
        IUrlParameters? tokenParams = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        string json = updateAcl.ToJson();
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/acl/update", json, tokenParams!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse DeleteAcl(string aclId, string? token)
    {
        IUrlParameters? tokenParams = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/acl/destroy/" + aclId, "", tokenParams!);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<Acl> GetAcl(string id)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/acl/info/" + id);

        if (httpResponse.StatusCode == 200)
        {
            IList<Acl> value = httpResponse.Content.FromJson<IList<Acl>>();

            if (value.Count == 0)
            {
                return new ConsulResponse<Acl>(null, httpResponse);
            }

            if (value.Count == 1)
            {
                return new ConsulResponse<Acl>(value[0], httpResponse);
            }
            throw new ConsulException("Strange response (list size=" + value.Count + ")");
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<string> CloneAcl(string aclId, string? token)
    {
        IUrlParameters? tokenParams = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/acl/clone/" + aclId, "", tokenParams!);

        if (httpResponse.StatusCode == 200)
        {
            IDictionary<string, string> value = httpResponse.Content.FromJson<IDictionary<string, string>>();
            return new ConsulResponse<string>(value["ID"], httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<Acl>> GetAclList(string? token)
    {
        IUrlParameters? tokenParams = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/acl/list", tokenParams!);

        if (httpResponse.StatusCode == 200)
        {
            IList<Acl> value = httpResponse.Content.FromJson<IList<Acl>>();
            return new ConsulResponse<IList<Acl>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }
}