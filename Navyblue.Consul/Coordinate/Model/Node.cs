using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Coordinate.Model;

/// <summary>
///
/// </summary>
public class Node
{
    [JsonProperty("node")]
    public string? NodeText { get; set; }

    [JsonProperty("coord")]
    public Coord? Coord { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}