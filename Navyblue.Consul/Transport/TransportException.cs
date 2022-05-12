namespace Navyblue.Consul.Transport;

/// <summary>
///
/// </summary>
public class TransportException : ConsulException
{
    public TransportException(Exception cause) : base(cause)
    {
    }
}