using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Catalog.Model;

/// <summary>
///
/// </summary>
public class CatalogNode
{
    public class CatalogService
    {
        [JsonProperty("id")]
        internal string? Id { get; set; }

        [JsonProperty("service")]
        internal string? Service { get; set; }

        [JsonProperty("tags")]
        internal IList<string>? Tags { get; set; }

        [JsonProperty("port")]
        internal int? Port { get; set; }

        public override string ToString()
        {
            return this.ToJson();
        }
    }

    [JsonProperty("node")]
    public Node? Node { get; set; }

    [JsonProperty("services")]
    public IDictionary<string, CatalogService>? Services { get; set; }

    public override string ToString()
    {
        return "CatalogNode{" + "node=" + Node + ", services=" + Services + '}';
    }
}