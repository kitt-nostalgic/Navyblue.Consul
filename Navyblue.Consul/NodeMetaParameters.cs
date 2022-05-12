namespace Navyblue.Consul;

public sealed class NodeMetaParameters : IUrlParameters
{
    private readonly IDictionary<string, string>? nodeMeta;

    public NodeMetaParameters(IDictionary<string, string>? nodeMeta)
    {
        this.nodeMeta = nodeMeta;
    }

    public IList<string> ToUrlParameters()
    {
        IList<string> @params = new List<string>();

        string key = "node-meta";

        foreach (KeyValuePair<string, string> entry in nodeMeta)
        {
            string value = entry.Key + ":" + entry.Value;
            @params.Add(key + "=" + value);
        }

        return @params;
    }

    public override bool Equals(object? o)
    {
        if (this == o)
        {
            return true;
        }
        if (!(o is NodeMetaParameters that))
        {
            return false;
        }

        return Equals(nodeMeta, that.nodeMeta);
    }

    public override int GetHashCode()
    {
        return nodeMeta.GetHashCode();
    }
}