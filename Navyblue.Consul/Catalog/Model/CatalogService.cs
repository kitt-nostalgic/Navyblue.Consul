using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Catalog.Model;

/// <summary>
///
/// </summary>
public class CatalogService
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("node")]
    public string? Node { get; set; }

    [JsonProperty("address")]
    public string? Address { get; set; }

    [JsonProperty("datacenter")]
    public string? Datacenter { get; set; }

    [JsonProperty("taggedAddresses")]
    public IDictionary<string, string>? TaggedAddresses { get; set; }

    [JsonProperty("nodeMeta")]
    public IDictionary<string, string>? NodeMeta { get; set; }

    [JsonProperty("serviceId")]
    public string? ServiceId { get; set; }

    [JsonProperty("serviceName")]
    public string? ServiceName { get; set; }

    [JsonProperty("serviceTags")]
    public IList<string>? ServiceTags { get; set; }

    [JsonProperty("serviceAddress")]
    public string? ServiceAddress { get; set; }

    [JsonProperty("serviceMeta")]
    public IDictionary<string, string>? ServiceMeta { get; set; }

    [JsonProperty("servicePort")]
    public int? ServicePort { get; set; }

    [JsonProperty("serviceEnableTagOverride")]
    public bool? ServiceEnableTagOverride { get; set; }

    [JsonProperty("createIndex")]
    public long? CreateIndex { get; set; }

    [JsonProperty("modifyIndex")]
    public long? ModifyIndex { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}