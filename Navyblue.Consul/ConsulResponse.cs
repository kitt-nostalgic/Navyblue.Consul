using Navyblue.BaseLibrary;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul;

/// <summary>
///
/// </summary>
public class ConsulResponse<T>
{
    public T? Value { get; }
    public long? ConsulIndex { get; }
    public bool? ConsulKnownLeader { get; }
    public long? ConsulLastContact { get; }

    public ConsulResponse(T? value, long? consulIndex, bool? consulKnownLeader, long? consulLastContact)
    {
        Value = value;
        ConsulIndex = consulIndex;
        ConsulKnownLeader = consulKnownLeader;
        ConsulLastContact = consulLastContact;
    }

    public ConsulResponse(T? value, ConsulHttpResponse httpResponse) : this(value, httpResponse.ConsulIndex, httpResponse.ConsulKnownLeader, httpResponse.ConsulLastContact)
    {

    }

    public override string ToString()
    {
        return this.ToJson();
    }
}

public sealed class ConsulResponse: ConsulResponse<object>
{
    public ConsulResponse(long? consulIndex, bool? consulKnownLeader, long? consulLastContact) : base(null, consulIndex, consulKnownLeader, consulLastContact)
    {
    }

    public ConsulResponse(ConsulHttpResponse httpResponse) : base(null, httpResponse)
    {
    }
}