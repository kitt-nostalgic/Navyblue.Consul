using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Agent.Model;

/// <summary>
///
/// </summary>
public class NewService
{
    public class Check
    {
        [JsonProperty("dockerContainerId")]
        public string? DockerContainerId { get; set; }

        [JsonProperty("shell")]
        public string? Shell { get; set; }

        [JsonProperty("interval")]
        public string? Interval { get; set; }

        [JsonProperty("ttl")]
        public string? Ttl { get; set; }

        [JsonProperty("http")]
        public string? Http { get; set; }

        [JsonProperty("method")]
        public string? Method { get; set; }

        [JsonProperty("header")]
        public IDictionary<string, IList<string>>? Header { get; set; }

        [JsonProperty("tcp")]
        public string? Tcp { get; set; }

        [JsonProperty("timeout")]
        public string? Timeout { get; set; }

        [JsonProperty("deregisterCriticalServiceAfter")]
        public string? DeregisterCriticalServiceAfter { get; set; }

        [JsonProperty("tlsSkipVerify")]
        public bool? TlsSkipVerify { get; set; }

        /// <summary>
        /// 'passing', 'warning', 'critical'
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("grpc")]
        public string? Grpc { get; set; }

        [JsonProperty("grpcUseTls")]
        public bool? GrpcUseTls { get; set; }

        public override string ToString()
        {
            return this.ToJson();
        }
    }

    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("tags")]
    public IList<string>? Tags { get; set; }

    [JsonProperty("address")]
    public string? Address { get; set; }

    [JsonProperty("meta")]
    public IDictionary<string, string>? Meta { get; set; }

    [JsonProperty("port")]
    public int? Port { get; set; }

    [JsonProperty("enableTagOverride")]
    public bool? EnableTagOverride { get; set; }

    [JsonProperty("check")]
    public Check? NewServiceCheck { get; set; }

    [JsonProperty("checks")]
    public IList<Check>? Checks { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}