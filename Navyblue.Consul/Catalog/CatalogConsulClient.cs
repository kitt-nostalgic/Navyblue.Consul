using Navyblue.BaseLibrary;
using Navyblue.Consul.Catalog.Model;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul.Catalog;

/// <summary>
///
/// </summary>
public sealed class CatalogConsulClient : ICatalogClient
{
    private readonly IConsulRawClient _consulRawClient;

    public CatalogConsulClient(IConsulRawClient consulRawClient)
    {
        this._consulRawClient = consulRawClient;
    }

    public ConsulResponse CatalogRegister(CatalogRegistration catalogRegistration)
    {
        return CatalogRegister(catalogRegistration, null);
    }

    public ConsulResponse CatalogRegister(CatalogRegistration catalogRegistration, string? token)
    {
        string json = catalogRegistration.ToJson();
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/catalog/register", json, tokenParam);
        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse CatalogDeregister(CatalogDeregistration catalogDeregistration)
    {
        return CatalogDeregister(catalogDeregistration, null);
    }

    public ConsulResponse CatalogDeregister(CatalogDeregistration catalogDeregistration, string? token)
    {
        string json = catalogDeregistration.ToJson();
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;

        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/catalog/deregister", json, tokenParam);
        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<string>> CatalogDataCenters
    {
        get
        {
            ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/catalog/datacenters");

            if (httpResponse.StatusCode == 200)
            {
                IList<string> value = httpResponse.Content.FromJson<List<string>>();
                return new ConsulResponse<IList<string>>(value, httpResponse);
            }

            throw new OperationException(httpResponse);
        }
    }

    public ConsulResponse<IList<Node>> GetCatalogNodes(QueryParams queryParams)
    {
        CatalogNodesRequest request = new CatalogNodesRequest
        {
            QueryParams =  queryParams
        };

        return GetCatalogNodes(request);
    }

    public ConsulResponse<IList<Node>> GetCatalogNodes(CatalogNodesRequest catalogNodesRequest)
    {
        Request request = new Request
        {
            Endpoint = "/v1/catalog/nodes",
            UrlParameters = catalogNodesRequest.AsUrlParameters()
        }; 

        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest(request);

        if (httpResponse.StatusCode == 200)
        {
            IList<Node> value = httpResponse.Content.FromJson<List<Node>>();
            return new ConsulResponse<IList<Node>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IDictionary<string, IList<string>>> GetCatalogServices(QueryParams queryParams)
    {
        return GetCatalogServices(queryParams, null);
    }

    public ConsulResponse<IDictionary<string, IList<string>>> GetCatalogServices(QueryParams queryParams, string? token)
    {
        CatalogServicesRequest request = new CatalogServicesRequest
        {
            QueryParams = queryParams,
            Token = token
        };

        return GetCatalogServices(request);
    }

    public ConsulResponse<IDictionary<string, IList<string>>> GetCatalogServices(CatalogServicesRequest catalogServicesRequest)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/catalog/services", catalogServicesRequest.AsUrlParameters());

        if (httpResponse.StatusCode == 200)
        {
            IDictionary<string, IList<string>> value = httpResponse.Content.FromJson<Dictionary<string, IList<string>>>();
            return new ConsulResponse<IDictionary<string, IList<string>>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<CatalogService>> GetCatalogService(string serviceName, QueryParams queryParams)
    {
        return GetCatalogService(serviceName, string.Empty, queryParams, string.Empty);
    }

    public ConsulResponse<IList<CatalogService>> GetCatalogService(string serviceName, QueryParams queryParams, string token)
    {
        return GetCatalogService(serviceName, string.Empty, queryParams, token);
    }

    public ConsulResponse<IList<CatalogService>> GetCatalogService(string serviceName, string tag, QueryParams queryParams)
    {
        return GetCatalogService(serviceName, tag, queryParams, string.Empty);
    }

    public ConsulResponse<IList<CatalogService>> GetCatalogService(string serviceName, string tag, QueryParams queryParams, string token)
    {
        return GetCatalogService(serviceName, new string[] { tag }, queryParams, null);
    }

    public ConsulResponse<IList<CatalogService>> GetCatalogService(string serviceName, string[]? tag, QueryParams queryParams, string? token)
    {
        CatalogServiceRequest request = new CatalogServiceRequest
        {
            Tags = tag,
            QueryParams = queryParams,
            Token = token
        };

        return GetCatalogService(serviceName, request);
    }

    public ConsulResponse<IList<CatalogService>> GetCatalogService(string serviceName, CatalogServiceRequest catalogServiceRequest)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/catalog/service/" + serviceName, catalogServiceRequest.AsUrlParameters());

        if (httpResponse.StatusCode == 200)
        {
            IList<CatalogService> value = httpResponse.Content.FromJson<List<CatalogService>>();
            return new ConsulResponse<IList<CatalogService>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<CatalogNode> GetCatalogNode(string nodeName, QueryParams? queryParams)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/catalog/node/" + nodeName, queryParams);

        if (httpResponse.StatusCode == 200)
        {
            CatalogNode catalogNode = httpResponse.Content.FromJson<CatalogNode>();
            return new ConsulResponse<CatalogNode>(catalogNode, httpResponse);
        }

        throw new OperationException(httpResponse);
    }
}