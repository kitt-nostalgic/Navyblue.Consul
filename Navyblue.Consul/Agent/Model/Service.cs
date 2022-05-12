using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Agent.Model;

/// <summary>
///
/// </summary>
public class Service
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("service")]
    public string? ServiceValue{ get; set; }

    [JsonProperty("tags")]
    public IList<string>? Tags{ get; set; }

    [JsonProperty("address")]
    public string? Address { get; set; }

    [JsonProperty("meta")]
    public IDictionary<string, string>? Meta { get; set; }

    [JsonProperty("port")]
    public int? Port{ get; set; }

    [JsonProperty("enableTagOverride")]
    public bool? EnableTagOverride{ get; set; }

    [JsonProperty("createIndex")]
    public long? CreateIndex{ get; set; }

    [JsonProperty("modifyIndex")]
    public long? ModifyIndex{ get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}