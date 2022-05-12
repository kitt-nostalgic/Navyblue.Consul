namespace Navyblue.Extensions.Configuration.Consul.Configs;

/// <summary>
/// 
/// </summary>
/// <seealso cref="Navyblue.Consul.ConsulConfiguration" />
public class ConsulConfigurationOptions : Navyblue.Consul.ConsulConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsulConfigurationOptions"/> class.
    /// </summary>
    public ConsulConfigurationOptions()
    {
        this.Configuration = new ConsulConfiguration();
    }

    /// <summary>
    /// Gets or sets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public ConsulConfiguration Configuration { get; set; }
}