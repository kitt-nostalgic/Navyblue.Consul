using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Catalog.Model;

/// <summary>
///
/// </summary>
public class CatalogRegistration
{
    public class Service
    {
        [JsonProperty("id")]
        internal string? Id{ get; set; }

        [JsonProperty("service")]
        internal string? ServiceText { get; set; }

        [JsonProperty("tags")]
        internal IList<string>? Tags{ get; set; }

        [JsonProperty("address")]
        internal string? Address{ get; set; }

        [JsonProperty("meta")]
        internal IDictionary<string, string>? Meta{ get; set; }

        [JsonProperty("port")]
        internal int? Port{ get; set; }

        public override string ToString()
        {
            return this.ToJson();
        }
    }

    public enum CheckStatus
    {
        Unknown,
        Passing,
        Warning,
        Critical
    }

    public class Check
    {
        [JsonProperty("node")]
        internal string? Node { get; set; }

        [JsonProperty("checkId")]
        internal string? CheckId { get; set; }

        [JsonProperty("name")]
        internal string? Name { get; set; }

        [JsonProperty("notes")]
        internal string? Notes { get; set; }

        [JsonProperty("status")]
        internal CheckStatus Status{ get; set; }

        [JsonProperty("serviceId")]
        internal string? ServiceId { get; set; }

        public override string ToString()
        {
            return this.ToJson();
        }
    }

    [JsonProperty("datacenter")]
    public string? Datacenter { get; set; }

    [JsonProperty("node")]
    public string? Node { get; set; }

    [JsonProperty("address")]
    public string? Address { get; set; }

    [JsonProperty("service")]
    public Service? ServiceText { get; set; }

    [JsonProperty("check")]
    public Check? CheckText { get; set; }

    [JsonProperty("writeRequest")]
    public WriteRequest? WriteRequest{ get; set; }

    [JsonProperty("nodeMeta")]
    public IDictionary<string, string>? NodeMeta { get; set; }

    [JsonProperty("skipNodeUpdate")]
    public bool SkipNodeUpdate{ get; set; }

    [JsonProperty("taggedAddresses")]
    public IDictionary<string, string>? TaggedAddresses { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}