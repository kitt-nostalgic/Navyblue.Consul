namespace Navyblue.Consul.Event;

public sealed class EventListRequest : ConsulRequest
{
    public string Name { get; }
    public string Node { get; }
    public string Service { get; }
    public string Tag { get; }
    public QueryParams? QueryParams { get; }
    public string Token { get; }

    public EventListRequest(string name, string node, string service, string tag, QueryParams? queryParams, string token)
    {
        Name = name;
        Node = node;
        Service = service;
        Tag = tag;
        QueryParams = queryParams;
        Token = token;
    }

    public IList<IUrlParameters?> AsUrlParameters()
    {
        IList<IUrlParameters?> @params = new List<IUrlParameters?>();

        if (!ReferenceEquals(Name, null))
        {
            @params.Add(new SingleUrlParameters("name", Name));
        }

        if (!ReferenceEquals(Node, null))
        {
            @params.Add(new SingleUrlParameters("node", Node));
        }

        if (!ReferenceEquals(Service, null))
        {
            @params.Add(new SingleUrlParameters("service", Service));
        }

        if (!ReferenceEquals(Tag, null))
        {
            @params.Add(new SingleUrlParameters("tag", Tag));
        }

        @params.Add(QueryParams);

        if (!ReferenceEquals(Token, null))
        {
            @params.Add(new SingleUrlParameters("token", Token));
        }

        return @params;
    }

    public override bool Equals(object o)
    {
        if (this == o)
        {
            return true;
        }
        if (!(o is EventListRequest that))
        {
            return false;
        }

        return Equals(Name, that.Name) && Equals(Node, that.Node) && Equals(Service, that.Service) && Equals(Tag, that.Tag) && Equals(QueryParams, that.QueryParams) && Equals(Token, that.Token);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Node.GetHashCode() ^ Service.GetHashCode() ^ Tag.GetHashCode() ^ QueryParams.GetHashCode() ^ Token.GetHashCode();
    }
}