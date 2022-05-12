namespace Navyblue.Consul;

public sealed class TagsParameters : IUrlParameters
{
    private readonly string[]? tags;

    public TagsParameters(string[]? tags)
    {
        this.tags = tags;
    }

    public IList<string> ToUrlParameters()
    {
        IList<string> @params = new List<string>();

        foreach (string tag in tags)
        {
            if (!string.IsNullOrWhiteSpace(tag))
            {
                @params.Add("tag=" + tag);
            }
        }

        return @params;
    }

    public override bool Equals(object o)
    {
        if (this == o)
        {
            return true;
        }
        if (!(o is TagsParameters that))
        {
            return false;
        }

        return Array.Equals(tags, that.tags);
    }

    public override int GetHashCode()
    {
        return tags.GetHashCode();
    }
}