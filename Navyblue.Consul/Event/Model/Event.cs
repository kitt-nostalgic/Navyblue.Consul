using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Event.Model;

/// <summary>
///
/// </summary>
public class Event
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("payload")]
    public string? Payload { get; set; }

    [JsonProperty("nodeFilter")]
    public string? NodeFilter { get; set; }

    [JsonProperty("serviceFilter")]
    public string? ServiceFilter { get; set; }

    [JsonProperty("tagFilter")]
    public string? TagFilter { get; set; }

    [JsonProperty("version")]
    public int Version { get; set; }

    [JsonProperty("lTime")]
    public int LTime { get; set; }

    /// <summary>
    /// Converted from https://github.com/hashicorp/consul/blob/master/api/event.go#L90-L104
    /// This is a hack. It simulates the index generation to convert an event ID into a WaitIndex.
    /// </summary>
    /// <returns> a Wait Index value suitable for passing in to <seealso cref="QueryParams"/>
    /// for blocking eventList calls. </returns>
    public virtual long WaitIndex
    {
        get
        {
            if (Id is not { Length: 36 })
            {
                return 0;
            }

            long lower = 0, upper = 0;
            for (int i = 0; i < 18; i++)
            {
                if (i != 8 && i != 13)
                {
                    lower = lower * 16 + Id[i];
                }
            }
            for (int i = 19; i < 36; i++)
            {
                if (i != 23)
                {
                    upper = upper * 16 + Id[i];
                }
            }
            return lower ^ upper;
        }
    }

    public override string ToString()
    {
        return this.ToJson();
    }
}