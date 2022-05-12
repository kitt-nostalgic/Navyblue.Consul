using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Session.Model;

/// <summary>
///
/// </summary>
public class Session
{
    [JsonProperty("lockDelay")]
    public long LockDelay { get; set; }

    [JsonProperty("checks")]
    public IList<string>? Checks { get; set; }

    [JsonProperty("node")]
    public string? Node { get; set; }

    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("createIndex")]
    public long CreateIndex { get; set; }

    [JsonProperty("modifyIndex")]
    public long ModifyIndex { get; set; }

    [JsonProperty("ttl")]
    public string? Ttl { get; set; }

    [JsonProperty("behavior")]
    public Behavior BehaviorType { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}