using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.KV.Model;

/// <summary>
///
/// </summary>
public class GetBinaryValue
{
    [JsonProperty("createIndex")]
    public long CreateIndex { get; set; }

    [JsonProperty("modifyIndex")]
    public long ModifyIndex { get; set; }

    [JsonProperty("lockIndex")]
    public long? LockIndex { get; set; }

    [JsonProperty("flags")]
    public long Flags { get; set; }

    [JsonProperty("session")]
    public string? Session { get; set; }

    [JsonProperty("key")]
    public string? Key { get; set; }

    [JsonProperty("value")]
    public sbyte[]? Value { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}