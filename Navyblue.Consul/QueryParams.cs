namespace Navyblue.Consul;

/// <summary>
///
/// </summary>
public sealed class QueryParams : IUrlParameters
{
    public sealed class Builder
    {
        public static Builder builder()
        {
            return new Builder();
        }

        internal string datacenter;
        internal ConsistencyMode consistencyMode;
        internal long waitTime;
        internal long index;
        internal string near;

        internal Builder()
        {
            datacenter = null;
            consistencyMode = ConsistencyMode.Default;
            waitTime = -1;
            index = -1;
            near = null;
        }

        public Builder setConsistencyMode(ConsistencyMode consistencyMode)
        {
            this.consistencyMode = consistencyMode;
            return this;
        }

        public Builder setDatacenter(string datacenter)
        {
            this.datacenter = datacenter;
            return this;
        }

        public Builder setWaitTime(long waitTime)
        {
            this.waitTime = waitTime;
            return this;
        }

        public Builder setIndex(long index)
        {
            this.index = index;
            return this;
        }

        public Builder setNear(string near)
        {
            this.near = near;
            return this;
        }

        public QueryParams? build()
        {
            return new QueryParams(datacenter, consistencyMode, waitTime, index, near);
        }
    }

    public static readonly QueryParams? DEFAULT = new QueryParams(ConsistencyMode.Default);

    private readonly string datacenter;
    private readonly ConsistencyMode consistencyMode;
    private readonly long waitTime;
    private readonly long index;
    private readonly string near;

    private QueryParams(string datacenter, ConsistencyMode consistencyMode, long waitTime, long index, string near)
    {
        this.datacenter = datacenter;
        this.consistencyMode = consistencyMode;
        this.waitTime = waitTime;
        this.index = index;
        this.near = near;
    }

    private QueryParams(string datacenter, ConsistencyMode consistencyMode, long waitTime, long index) : this(datacenter, consistencyMode, waitTime, index, null)
    {
    }

    public QueryParams(string datacenter) : this(datacenter, ConsistencyMode.Default, -1, -1)
    {
    }

    public QueryParams(ConsistencyMode consistencyMode) : this(null, consistencyMode, -1, -1)
    {
    }

    public QueryParams(string datacenter, ConsistencyMode consistencyMode) : this(datacenter, consistencyMode, -1, -1)
    {
    }

    public QueryParams(long waitTime, long index) : this(null, ConsistencyMode.Default, waitTime, index)
    {
    }

    public QueryParams(string datacenter, long waitTime, long index) : this(datacenter, ConsistencyMode.Default, waitTime, index, null)
    {
    }

    public string Datacenter
    {
        get
        {
            return datacenter;
        }
    }

    public ConsistencyMode ConsistencyMode
    {
        get
        {
            return consistencyMode;
        }
    }

    public long WaitTime
    {
        get
        {
            return waitTime;
        }
    }

    public long Index
    {
        get
        {
            return index;
        }
    }

    public string Near
    {
        get
        {
            return near;
        }
    }

    public IList<string> ToUrlParameters()
    {
        IList<string> @params = new List<string>();

        // add basic params
        if (!ReferenceEquals(datacenter, null))
        {
            @params.Add("dc=" + Utils.EncodeValue(datacenter));
        }

        if (consistencyMode != ConsistencyMode.Default)
        {
            @params.Add(consistencyMode.ToString().ToLower());
        }

        if (waitTime != -1)
        {
            @params.Add("wait=" + Utils.ToSecondsString(waitTime));
        }

        if (index != -1)
        {
            @params.Add("index=" + index);
        }

        if (!ReferenceEquals(near, null))
        {
            @params.Add("near=" + Utils.EncodeValue(near));
        }

        return @params;
    }

    public override bool Equals(object o)
    {
        if (this == o)
        {
            return true;
        }
        if (!(o is QueryParams that))
        {
            return false;
        }

        return waitTime == that.waitTime && index == that.index && Equals(datacenter, that.datacenter) && consistencyMode == that.consistencyMode && Equals(near, that.near);
    }

    public override int GetHashCode()
    {
        return datacenter.GetHashCode() ^ consistencyMode.GetHashCode() ^ waitTime.GetHashCode() ^ index.GetHashCode() ^ near.GetHashCode();
    }
}