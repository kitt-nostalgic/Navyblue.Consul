using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Health.Model;

/// <summary>
///
/// </summary>
public class Check
{
    [JsonProperty("node")]
    public string? Node { get; set; }

    [JsonProperty("checkId")]
    public string? CheckId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("status")]
    public CheckStatus Status { get; set; }

    [JsonProperty("notes")]
    public string? Notes { get; set; }

    [JsonProperty("output")]
    public string? Output { get; set; }

    [JsonProperty("serviceId")]
    public string? ServiceId { get; set; }

    [JsonProperty("serviceName")]
    public string? ServiceName { get; set; }

    [JsonProperty("serviceTags")]
    public IList<string>? ServiceTags { get; set; }

    [JsonProperty("createIndex")]
    public long? CreateIndex { get; set; }

    [JsonProperty("modifyIndex")]
    public long? ModifyIndex { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}