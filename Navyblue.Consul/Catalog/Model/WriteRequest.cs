using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Catalog.Model;

/// <summary>
///
/// </summary>
public class WriteRequest
{
    [JsonProperty("token")]
    public  string? Token { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}