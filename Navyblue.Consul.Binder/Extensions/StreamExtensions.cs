using System.Text;

namespace Navyblue.Extensions.Configuration.Consul.Extensions;

/// <summary>
/// 
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Converts to stream.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static MemoryStream ToStream(this string? value)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
    }
}