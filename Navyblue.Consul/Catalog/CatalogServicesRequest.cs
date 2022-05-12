using System.Collections.Immutable;

namespace Navyblue.Consul.Catalog;

public sealed class CatalogServicesRequest : ConsulRequest
{
    public string DataCenter { get; set; }

    public IDictionary<string, string>? NodeMeta { get; set; }

    public QueryParams? QueryParams { get; set; }

    public string Token { get; set; }

    public IList<IUrlParameters?> AsUrlParameters()
    {
        IList<IUrlParameters?> urlParameters = new List<IUrlParameters?>();

        if (!ReferenceEquals(DataCenter, null))
        {
            urlParameters.Add(new SingleUrlParameters("dc", DataCenter));
        }

        if (NodeMeta != null)
        {
            urlParameters.Add(new NodeMetaParameters(NodeMeta));
        }

        urlParameters.Add(QueryParams);

        if (!string.IsNullOrWhiteSpace(Token))
        {
            urlParameters.Add(new SingleUrlParameters("token", Token));
        }

        return urlParameters;
    }
}