using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Navyblue.Consul.KV;
using Navyblue.Extensions.Configuration.Consul.Configs;

namespace Navyblue.Extensions.Configuration.Consul
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Navyblue.Extensions.Configuration.Consul.IConsulConfigurationSource" />
    internal sealed class ConsulConfigurationSource : IConsulConfigurationSource
    {
        /// <summary>
        /// The key value client
        /// </summary>
        private readonly IKeyValueClient _keyValueClient;
        /// <summary>
        /// The consul configuration options
        /// </summary>
        private ConsulConfigurationOptions _consulConfigurationOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsulConfigurationSource"/> class.
        /// </summary>
        /// <param name="keyValueClient">The key value client.</param>
        /// <param name="configureNamedOptions">The configure named options.</param>
        public ConsulConfigurationSource(IKeyValueClient keyValueClient,
            IOptions<ConsulConfigurationOptions> configureNamedOptions)
        {
            _consulConfigurationOptions = configureNamedOptions.Value;
            _keyValueClient = keyValueClient;
        }

        /// <summary>
        /// Builds the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConsulConfigurationProvider(_keyValueClient, _consulConfigurationOptions, this);
        }
    }
}