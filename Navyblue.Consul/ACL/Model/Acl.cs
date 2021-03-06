using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.ACL.Model;

/// <summary>
///
/// </summary>
public class Acl
{
    [JsonProperty("createIndex")]
    public long CreateIndex { get; set; }

    [JsonProperty("modifyIndex")]
    public long ModifyIndex { get; set; }

    [JsonProperty("id")]
    public string? Id { get; set; }

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