using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Catalog.Model;

/// <summary>
///
/// </summary>
public class CatalogDeregistration
{
    [JsonProperty("datacenter")]
    public string? Datacenter { get; set; }

    [JsonProperty("node")]
    public string? Node { get; set; }

    [JsonProperty("checkId")]
    public string? CheckId { get; set; }

    [JsonProperty("serviceId")]
    public string? ServiceId { get; set; }

    [JsonProperty("writeRequest")]
    public WriteRequest? WriteRequest { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}