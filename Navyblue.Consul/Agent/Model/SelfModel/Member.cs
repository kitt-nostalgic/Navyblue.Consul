using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Agent.Model.SelfModel;

/// <summary>
///
/// </summary>
public class Member
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("address")]
    public string? Address { get; set; }

    [JsonProperty("port")]
    public int? Port { get; set; }

    [JsonProperty("tags")]
    public IDictionary<string, string>? Tags { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("protocolMin")]
    public int ProtocolMin { get; set; }

    [JsonProperty("protocolMax")]
    public int ProtocolMax { get; set; }

    [JsonProperty("protocolCur")]
    public int ProtocolCur { get; set; }

    [JsonProperty("delegateMin")]
    public int DelegateMin { get; set; }

    [JsonProperty("delegateMax")]
    public int DelegateMax { get; set; }

    [JsonProperty("delegateCur")]
    public int DelegateCur { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}