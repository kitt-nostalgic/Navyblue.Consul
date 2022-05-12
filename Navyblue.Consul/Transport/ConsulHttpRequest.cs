namespace Navyblue.Consul.Transport;

public sealed class ConsulHttpRequest
{
    public string? Url { get; set; }

    public IDictionary<string, string?>? Headers { get; set; }

    public string? Content { get; set; }

    public sbyte[]? BinaryContent { get; set; }
}