using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Coordinate.Model;

/// <summary>
///
/// </summary>
public class Coord
{
    [JsonProperty("error")]
    public double? Error { get; set; }

    [JsonProperty("height")]
    public double? Height { get; set; }

    [JsonProperty("adjustment")]
    public double? Adjustment { get; set; }

    [JsonProperty("vec")]
    public IList<double?>? Vec { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}