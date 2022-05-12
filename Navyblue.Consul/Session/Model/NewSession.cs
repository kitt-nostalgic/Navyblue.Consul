using Newtonsoft.Json;

namespace Navyblue.Consul.Session.Model;

/// <summary>
///
/// </summary>
public class NewSession
{
    [JsonProperty("lockDelay")]
    public long LockDelay { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("node")]
    public string? Node { get; set; }

    [JsonProperty("checks")]
    public IList<string>? Checks { get; set; }

    [JsonProperty("behavior")]
    public Behavior Behavior { get; set; }

    [JsonProperty("ttl")]
    public string? Ttl { get; set; }
}