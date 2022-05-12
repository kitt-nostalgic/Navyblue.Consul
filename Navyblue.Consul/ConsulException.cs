namespace Navyblue.Consul;

/// <summary>
/// Base exception for any consul errors
///
///
/// </summary>
public class ConsulException : Exception
{
    public ConsulException()
    {
    }

    public ConsulException(Exception cause) : base(cause.Message)
    {
    }

    public ConsulException(string message) : base(message)
    {
    }
}