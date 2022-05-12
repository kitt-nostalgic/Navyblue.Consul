namespace Navyblue.Consul.Event.Model;

/// <summary>
///
/// </summary>
public class EventParams : IUrlParameters
{
    public string Name { get; set; }
    public string Service { get; set; }
    public string Tag { get; set; }
    public string Node { get; set; }

    public virtual IList<string> ToUrlParameters()
    {
        IList<string> result = new List<string>();

        if (!ReferenceEquals(Name, null))
        {
            result.Add("name=" + Utils.EncodeValue(Name));
        }

        if (!ReferenceEquals(Service, null))
        {
            result.Add("service=" + Utils.EncodeValue(Service));
        }

        if (!ReferenceEquals(Tag, null))
        {
            result.Add("tag=" + Utils.EncodeValue(Tag));
        }

        if (!ReferenceEquals(Node, null))
        {
            result.Add("node=" + Utils.EncodeValue(Node));
        }

        return result;
    }

    public override bool Equals(object? o)
    {
        if (this == o)
        {
            return true;
        }
        if (!(o is EventParams that))
        {
            return false;
        }

        return Equals(Name, that.Name) && Equals(Service, that.Service) && Equals(Tag, that.Tag) && Equals(Node, that.Node);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Service.GetHashCode() ^ Tag.GetHashCode() ^ Node.GetHashCode(); 
    }
}