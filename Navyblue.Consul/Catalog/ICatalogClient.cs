using Navyblue.Consul.Catalog.Model;

namespace Navyblue.Consul.Catalog;

/// <summary>
///
/// </summary>
public interface ICatalogClient
{
    ConsulResponse CatalogRegister(CatalogRegistration catalogRegistration);

    ConsulResponse CatalogRegister(CatalogRegistration catalogRegistration, string? token);

    ConsulResponse CatalogDeregister(CatalogDeregistration catalogDeregistration);

    ConsulResponse CatalogDeregister(CatalogDeregistration catalogDeregistration, string? token);

    ConsulResponse<IList<string>> CatalogDataCenters { get; }

    ConsulResponse<IList<Node>> GetCatalogNodes(CatalogNodesRequest catalogNodesRequest);

    ConsulResponse<IDictionary<string, IList<string>>> GetCatalogServices(CatalogServicesRequest catalogServicesRequest);

    ConsulResponse<IList<CatalogService>> GetCatalogService(string serviceName, CatalogServiceRequest catalogServiceRequest);

    ConsulResponse<CatalogNode> GetCatalogNode(string nodeName, QueryParams? queryParams);
}