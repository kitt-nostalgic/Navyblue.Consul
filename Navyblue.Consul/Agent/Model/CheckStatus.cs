using System.ComponentModel;

namespace Navyblue.Consul.Agent.Model;

public enum CheckStatus
{
    [Description("passing")]
    Passing,

    [Description("warning")]
    Warning,

    [Description("critical")]
    Critical
}