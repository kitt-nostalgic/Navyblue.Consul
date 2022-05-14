using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Navyblue.Consul.KV;
using Navyblue.Extensions.Configuration.Consul.Configs;
using Serilog;

namespace Navyblue.Extensions.Configuration.Consul
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Navyblue.Extensions.Configuration.Consul.IConsulConfigurationSource" />
    internal sealed class ConsulConfigurationSource : IConsulConfigurationSource
    {
        private readonly IKeyValueClient _keyValueClient;
        private ConsulConfigurationOptions _consulConfigurationOptions;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsulConfigurationSource"/> class.
        /// </summary>
        /// <param name="keyValueClient">The key value client.</param>
        /// <param name="configureNamedOptions">The configure named options.</param>
        /// <param name="logger"></param>
        public ConsulConfigurationSource(IKeyValueClient keyValueClient,
            IOptions<ConsulConfigurationOptions> configureNamedOptions,
            ILogger logger)
        {
            _consulConfigurationOptions = configureNamedOptions.Value;
            _keyValueClient = keyValueClient;
            _logger = logger.ForContext<ConsulConfigurationSource>();
        }

        /// <summary>
        /// Builds the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConsulConfigurationProvider(_keyValueClient, _consulConfigurationOptions, _logger);
        }
    }
}