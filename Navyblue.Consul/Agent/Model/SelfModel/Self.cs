using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Agent.Model.SelfModel;

/// <summary>
///
/// </summary>
public class Self
{
    [JsonProperty("config")]
    public Config? Config { get; set; }

    [JsonProperty("selfDebugConfig")]
    public DebugConfig? DebugConfig { get; set; }

    [JsonProperty("member")]
    public Member? Member { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}