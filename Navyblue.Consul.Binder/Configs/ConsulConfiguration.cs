namespace Navyblue.Extensions.Configuration.Consul.Configs;

/// <summary>
/// 
/// </summary>
public class ConsulConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether [enabled refresh].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [enabled refresh]; otherwise, <c>false</c>.
    /// </value>
    public bool EnabledRefresh { get; set; } = true;

    /// <summary>
    /// Gets or sets the refresh interval.
    /// Default 5s
    /// </summary>
    /// <value>
    /// The refresh interval.
    /// </value>
    public int RefreshInterval { get; set; } = 5;

    /// <summary>
    /// Gets or sets the format.
    /// </summary>
    /// <value>
    /// The format.
    /// </value>
    public ConsulConfigurationFormat Format { get; set; }

    /// <summary>
    /// Gets or sets the data key.
    /// </summary>
    /// <value>
    /// The data key.
    /// </value>
    public string DataKey { get; set; } = "Configuration";
}