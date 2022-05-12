namespace Navyblue.Consul.Transport;

/// <summary>
///
/// </summary>
public sealed class ConsulHttpResponse
{
    public int StatusCode { get; set; }

    public string? StatusMessage { get; set; }

    public string Content { get; set; }

    public long? ConsulIndex { get; set; }

    public bool? ConsulKnownLeader { get; set; }

    public long? ConsulLastContact { get; set; }
}