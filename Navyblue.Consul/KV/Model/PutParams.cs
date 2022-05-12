namespace Navyblue.Consul.KV.Model;

/// <summary>
///
/// </summary>
public class PutParams : IUrlParameters
{
    public long Flags { get; set; }

    public long? Cas { get; set; }

    public string AcquireSession { get; set; }

    public string ReleaseSession { get; set; }

    public virtual IList<string> ToUrlParameters()
    {
        IList<string> lst = new List<string>();

        if (Flags != 0)
        {
            lst.Add("flags=" + Flags);
        }
        if (Cas != null)
        {
            lst.Add("cas=" + Cas);
        }
        if (!ReferenceEquals(AcquireSession, null))
        {
            lst.Add("acquire=" + Utils.EncodeValue(AcquireSession));
        }
        if (!ReferenceEquals(ReleaseSession, null))
        {
            lst.Add("release=" + Utils.EncodeValue(ReleaseSession));
        }

        return lst;
    }
}