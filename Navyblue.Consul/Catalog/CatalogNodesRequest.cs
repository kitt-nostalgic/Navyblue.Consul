using System.Collections.Immutable;

namespace Navyblue.Consul.Catalog;

public sealed class CatalogNodesRequest : ConsulRequest
{
    public string Datacenter { get; set; }

    public string Near { get; set; }

    public IDictionary<string, string>? NodeMeta { get; set; }

    public QueryParams? QueryParams { get; set; }

    public IList<IUrlParameters?> AsUrlParameters()
    {
        IList<IUrlParameters?> urlParameters = new List<IUrlParameters?>();

        if (!ReferenceEquals(Datacenter, null))
        {
            urlParameters.Add(new SingleUrlParameters("dc", Datacenter));
        }

        if (!ReferenceEquals(Near, null))
        {
            urlParameters.Add(new SingleUrlParameters("near", Near));
        }

        urlParameters.Add(new NodeMetaParameters(NodeMeta));

        urlParameters.Add(QueryParams);

        return urlParameters;
    }
}