namespace Navyblue.Consul;

public sealed class Request
{
    public string Endpoint { get; set; }

    public IList<IUrlParameters?>? UrlParameters { get; set; }

    public string Content { get; set; }

    public sbyte[] BinaryContent { get; set; }

    public string? Token { get; set; }
}