
namespace Navyblue.Consul.Health;

public sealed class HealthChecksForServiceRequest : ConsulRequest
{
    public string Datacenter { get; set; }

    public string Near { get; set; }

    public IDictionary<string, string>? NodeMeta { get; set; }

    public QueryParams? QueryParams { get; set; }

    public IList<IUrlParameters?> AsUrlParameters()
    {
        IList<IUrlParameters?> @params = new List<IUrlParameters?>();

        if (!ReferenceEquals(Datacenter, null))
        {
            @params.Add(new SingleUrlParameters("dc", Datacenter));
        }

        if (!ReferenceEquals(Near, null))
        {
            @params.Add(new SingleUrlParameters("near", Near));
        }

        @params.Add(new NodeMetaParameters(NodeMeta));

        @params.Add(QueryParams);

        return @params;
    }
}