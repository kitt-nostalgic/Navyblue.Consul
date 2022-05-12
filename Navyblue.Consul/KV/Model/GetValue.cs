using System.Text;
using Navyblue.BaseLibrary;

namespace Navyblue.Consul.KV.Model;

/// <summary>
///
/// </summary>
public class GetValue
{
    public long CreateIndex { get; set; }

    public long ModifyIndex { get; set; }

    public long? LockIndex { get; set; }

    public long Flags { get; set; }

    public string? Session { get; set; }

    public string? Key { get; set; }

    public string? Value { get; set; }

    public virtual string? DecodedValue
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return null;
            }

            var base64EncodedBytes = Convert.FromBase64String(Value);

            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }

    public override string ToString()
    {
        return this.ToJson();
    }
}