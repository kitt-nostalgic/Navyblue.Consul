namespace Navyblue.Consul;

/// <summary>
///
/// </summary>
public sealed class SingleUrlParameters : IUrlParameters
{
    private readonly string key;
    private readonly string value;

    public SingleUrlParameters(string key)
    {
        this.key = key;
        value = null;
    }

    public SingleUrlParameters(string key, string value)
    {
        this.key = key;
        this.value = value;
    }

    public IList<string> ToUrlParameters()
    {
        if (!ReferenceEquals(value, null))
        {
            return new List<string>() { key + "=" + Utils.EncodeValue(value) };
        }

        return new List<string>() { key };
    }

    public override bool Equals(object o)
    {
        if (this == o)
        {
            return true;
        }
        if (!(o is SingleUrlParameters that))
        {
            return false;
        }

        return Equals(key, that.key) && Equals(value, that.value);
    }

    public override int GetHashCode()
    {
        return key.GetHashCode() ^ value.GetHashCode();
    }
}