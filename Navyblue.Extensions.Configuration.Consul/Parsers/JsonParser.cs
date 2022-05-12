using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Navyblue.BaseLibrary;

namespace Navyblue.Extensions.Configuration.Consul.Parsers;

public class JsonParser
{
    /// <summary>
    /// Parses the stream.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns></returns>
    /// <exception cref="System.FormatException">Top-level JSON element must be an object. Instead, '{0}' was found.".FormatWith(doc.RootElement.ValueKind)</exception>
    public static IDictionary<string, string?> ParseStream(Stream input)
    {
        var jsonDocumentOptions = new JsonDocumentOptions
        {
            CommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
        };

        using (var reader = new StreamReader(input))
        using (JsonDocument doc = JsonDocument.Parse(reader.ReadToEnd(), jsonDocumentOptions))
        {
            if (doc.RootElement.ValueKind != JsonValueKind.Object)
            {
                throw new FormatException("Top-level JSON element must be an object. Instead, '{0}' was found.".FormatWith(doc.RootElement.ValueKind));
            }
            VisitElement(doc.RootElement);
        }

        return _data;
    }

    /// <summary>
    /// Visits the element.
    /// </summary>
    /// <param name="element">The element.</param>
    private static void VisitElement(JsonElement element)
    {
        var isEmpty = true;

        foreach (JsonProperty property in element.EnumerateObject())
        {
            isEmpty = false;
            EnterContext(property.Name);
            VisitValue(property.Value);
            ExitContext();
        }

        if (isEmpty && _paths.Count > 0)
        {
            _data[_paths.Peek()] = null;
        }
    }

    /// <summary>
    /// Visits the value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <exception cref="System.FormatException">
    /// A duplicate key '{0}' was found.".FormatWith(key)
    /// or
    /// Unsupported JSON token '{0}' was found.".FormatWith(value.ValueKind)
    /// </exception>
    private static void VisitValue(JsonElement value)
    {
        switch (value.ValueKind)
        {
            case JsonValueKind.Object:
                VisitElement(value);
                break;

            case JsonValueKind.Array:
                int index = 0;
                foreach (JsonElement arrayElement in value.EnumerateArray())
                {
                    EnterContext(index.ToString());
                    VisitValue(arrayElement);
                    ExitContext();
                    index++;
                }
                break;

            case JsonValueKind.Number:
            case JsonValueKind.String:
            case JsonValueKind.True:
            case JsonValueKind.False:
            case JsonValueKind.Null:
                string key = _paths.Peek();
                if (_data.ContainsKey(key))
                {
                    throw new FormatException("A duplicate key '{0}' was found.".FormatWith(key));
                }
                _data[key] = value.ToString();
                break;

            default:
                throw new FormatException("Unsupported JSON token '{0}' was found.".FormatWith(value.ValueKind));
        }
    }

    /// <summary>
    /// Enters the context.
    /// </summary>
    /// <param name="context">The context.</param>
    private static void EnterContext(string context) =>
        _paths.Push(_paths.Count > 0 ?
            _paths.Peek() + ConfigurationPath.KeyDelimiter + context :
            context);

    /// <summary>
    /// Exits the context.
    /// </summary>
    private static void ExitContext() => _paths.Pop();

    /// <summary>
    /// The data
    /// </summary>
    private static readonly Dictionary<string, string?> _data = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// The paths
    /// </summary>
    private static readonly Stack<string> _paths = new();
}