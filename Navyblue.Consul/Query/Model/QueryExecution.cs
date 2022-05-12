using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Query.Model;

public class QueryExecution
{
    public class Dns
    {
        [JsonProperty("ttl")]
        internal string? Ttl { get; set; }

        public override string ToString()
        {
            return this.ToJson();
        }
    }

    [JsonProperty("service")]
    public string? Service { get; set; }

    [JsonProperty("nodes")]
    public IList<QueryNode>? Nodes { get; set; }

    [JsonProperty("dns")]
    public Dns? DNS { get; set; }

    [JsonProperty("datacenter")]
    public string? Datacenter { get; set; }

    [JsonProperty("failovers")]
    public int? Failovers { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}