using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Agent.Model;

public class Config
{
    [JsonProperty("datacenter")]
    public string? Datacenter { get; set; }

    [JsonProperty("nodeName")]
    public string? NodeName { get; set; }

    [JsonProperty("revision")]
    public string? Revision { get; set; }

    [JsonProperty("server")]
    public bool Server { get; set; }

    [JsonProperty("version")]
    public string? Version { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}