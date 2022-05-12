using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Coordinate.Model;

/// <summary>
///
/// </summary>
public class Datacenter
{
    [JsonProperty("datacenter")]
    public string? DataCenter { get; set; }

    [JsonProperty("areaId")]
    public string? AreaId { get; set; }

    [JsonProperty("coordinates")]
    public IList<Node>? Coordinates { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}