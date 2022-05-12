using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Query.Model;

public class QueryNode
{
    [JsonProperty("node")]
    public Node? Node { get; set; }

    [JsonProperty("service")]
    public Service? Service { get; set; }

    [JsonProperty("checks")]
    public IList<Check>? Checks { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}