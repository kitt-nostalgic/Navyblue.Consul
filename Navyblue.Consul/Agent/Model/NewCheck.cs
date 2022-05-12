using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Agent.Model;

/// <summary>
///
/// </summary>
public class NewCheck
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("serviceId")]
    public string? ServiceId { get; set; }

    [JsonProperty("notes")]
    public string? Notes { get; set; }

    [JsonProperty("args")]
    public IList<string>? Args { get; set; }

    [JsonProperty("http")]
    public string? Http { get; set; }

    [JsonProperty("method")]
    public string? Method { get; set; }

    [JsonProperty("header")]
    public IDictionary<string, IList<string>>? Header { get; set; }

    [JsonProperty("tcp")]
    public string? Tcp { get; set; }

    [JsonProperty("dockerContainerId")]
    public string? DockerContainerId { get; set; }

    [JsonProperty("shell")]
    public string? Shell { get; set; }

    [JsonProperty("interval")]
    public string? Interval { get; set; }

    [JsonProperty("timeout")]
    public string? Timeout { get; set; }

    [JsonProperty("ttl")]
    public string? Ttl { get; set; }

    [JsonProperty("deregisterCriticalServiceAfter")]
    public string? DeregisterCriticalServiceAfter { get; set; }

    [JsonProperty("tlsSkipVerify")]
    public bool? TlsSkipVerify{ get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("grpc")]
    public string? Grpc { get; set; }

    [JsonProperty("grpcUseTls")]
    public bool? GrpcUseTls{ get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}