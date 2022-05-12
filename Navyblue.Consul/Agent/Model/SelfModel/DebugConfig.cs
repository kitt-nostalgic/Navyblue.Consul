using Navyblue.BaseLibrary;
using Newtonsoft.Json;

namespace Navyblue.Consul.Agent.Model.SelfModel;

public class DebugConfig
{
    [JsonProperty("bootstrap")]
    public bool Bootstrap { get; set; }

    [JsonProperty("dataDir")]
    public string? DataDir { get; set; }

    [JsonProperty("dnsRecursor")]
    public string? DnsRecursor { get; set; }

    [JsonProperty("dnsDomain")]
    public string? DnsDomain { get; set; }

    [JsonProperty("logLevel")]
    public LogLevelType LogLevel { get; set; }

    [JsonProperty("nodeId")]
    public string? NodeId { get; set; }

    [JsonProperty("clientAddresses")]
    public string[]? ClientAddresses { get; set; }

    [JsonProperty("bindAddress")]
    public string? BindAddress { get; set; }

    [JsonProperty("leaveOnTerm")]
    public bool LeaveOnTerm { get; set; }

    [JsonProperty("skipLeaveOnInt")]
    public bool SkipLeaveOnInt { get; set; }

    [JsonProperty("enableDebug")]
    public bool EnableDebug { get; set; }

    [JsonProperty("verifyIncoming")]
    public bool VerifyIncoming { get; set; }

    [JsonProperty("verifyOutgoing")]
    public bool VerifyOutgoing { get; set; }

    [JsonProperty("caFile")]
    public string? CaFile { get; set; }

    [JsonProperty("certFile")]
    public string? CertFile { get; set; }

    [JsonProperty("keyFile")]
    public string? KeyFile { get; set; }

    [JsonProperty("uiDir")]
    public string? UiDir { get; set; }

    [JsonProperty("pidFile")]
    public string? PidFile { get; set; }

    [JsonProperty("enableSyslog")]
    public bool EnableSyslog { get; set; }

    [JsonProperty("rejoinAfterLeave")]
    public bool RejoinAfterLeave { get; set; }

    public override string ToString()
    {
        return this.ToJson();
    }
}