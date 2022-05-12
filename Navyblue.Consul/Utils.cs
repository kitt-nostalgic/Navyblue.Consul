using System.Text;
using System.Text.Encodings.Web;

namespace Navyblue.Consul;

/// <summary>
///
/// </summary>
public class Utils
{
    /// <summary>
    /// Encodes the value.
    /// , "UTF-8"
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">So strange - every JVM has to support UTF-8 encoding.</exception>
    public static string EncodeValue(string value)
    {
        try
        {
            return UrlEncoder.Default.Encode(value);
        }
        catch (EncoderFallbackException)
        {
            throw new Exception("So strange - every JVM has to support UTF-8 encoding.");
        }
    }

    public static string EncodeUrl(string str)
    {
        try
        {
            Uri uri = new Uri(str);
            return uri.ToString();
        }
        catch (Exception e)
        {
            throw new Exception("Can't encode url", e);
        }
    }

    public static string GenerateUrl(string baseUrl, IList<IUrlParameters?>? @params)
    {
        if (@params == null)
        {
            return baseUrl;
        }

        IList<string> allParams = new List<string>();
        foreach (IUrlParameters? item in @params)
        {
            ((List<string>)allParams).AddRange(item.ToUrlParameters());
        }

        // construct the whole url
        StringBuilder result = new StringBuilder(baseUrl);

        IEnumerator<string> paramsIterator = allParams.GetEnumerator();
        result.Append("?").Append(paramsIterator.MoveNext());
        while (paramsIterator.MoveNext())
        {
            result.Append("&").Append(paramsIterator.Current);
        }

        return result.ToString();
    }

    public static Dictionary<string, string?> CreateTokenMap(string? token)
    {
        Dictionary<string, string?> headers = new Dictionary<string, string?>
        {
            ["X-Consul-Token"] = token
        };
        return headers;
    }

    public static string ToSecondsString(long waitTime)
    {
        return waitTime + "s";
    }

    public static string AssembleAgentAddress(string host, int port, string path)
    {
        string agentPath = "";
        if (!ReferenceEquals(path, null) && path.Trim().Length > 0)
        {
            agentPath = "/" + path;
        }

        return $"http://{host}:{port:D}{agentPath}";
    }
}