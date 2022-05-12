using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Query.Model;

public class Node
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("node")]
    public string? NodeText { get; set; }

    [JsonProperty("address")]
    public string? Address { get; set; }

    [JsonProperty("datacenter")]
    public string? Datacenter { get; set; }

    [JsonProperty("taggedAddresses")]
    public IDictionary<string, string>? TaggedAddresses { get; set; }

    [JsonProperty("meta")]
    public IDictionary<string, string>? Meta { get; set; }

    [JsonProperty("createIndex")]
    public long? CreateIndex { get; set; }

    [JsonProperty("modifyIndex")]
    public long? ModifyIndex { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}