using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.ACL.Model;

/// <summary>
///
/// </summary>
public class CreateAcl
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("type")]
    public AclType AclType { get; set; }

    [JsonProperty("rules")]
    public string? Rules { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}