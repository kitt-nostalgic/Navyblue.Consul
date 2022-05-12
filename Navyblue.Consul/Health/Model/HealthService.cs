using Newtonsoft.Json;

namespace Navyblue.Consul.Health.Model;

/// <summary>
///
/// </summary>
public class HealthService
{
    [JsonProperty("node")]
    public Node? Node { get; set; }

    [JsonProperty("service")]
    public Service? Service { get; set; }

    [JsonProperty("checks")]
    public IList<Check>? Checks { get; set; }

    public override string ToString()
    {
        return "HealthService{" + "node=" + Node + ", service=" + Service + ", checks=" + Checks + '}';
    }
}