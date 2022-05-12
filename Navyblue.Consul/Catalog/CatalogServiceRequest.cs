using System.Collections.Immutable;

namespace Navyblue.Consul.Catalog;

public sealed class CatalogServiceRequest : ConsulRequest
{
    public string Datacenter { get; set; }

    public string[]? Tags { get; set; }

    public string Near { get; set; }

    public IDictionary<string, string>? NodeMeta { get; set; }

    public QueryParams QueryParams { get; set; }

    public string Token { get; set; }

    public IList<IUrlParameters?> AsUrlParameters()
    {
        IList<IUrlParameters?> urlParameters = new List<IUrlParameters?>();

        if (!ReferenceEquals(Datacenter, null))
        {
            urlParameters.Add(new SingleUrlParameters("dc", Datacenter));
        }

        urlParameters.Add(new TagsParameters(Tags));

        if (!ReferenceEquals(Near, null))
        {
            urlParameters.Add(new SingleUrlParameters("near", Near));
        }

        urlParameters.Add(new NodeMetaParameters(NodeMeta));

        urlParameters.Add(QueryParams);

        if (!ReferenceEquals(Token, null))
        {
            urlParameters.Add(new SingleUrlParameters("token", Token));
        }

        return urlParameters;
    }
}