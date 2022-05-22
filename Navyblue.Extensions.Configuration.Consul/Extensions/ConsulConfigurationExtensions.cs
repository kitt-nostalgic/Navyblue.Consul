using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Navyblue.Consul;
using Navyblue.Consul.KV;
using Navyblue.Extensions.Configuration.Consul.Configs;
using Serilog;

namespace Navyblue.Extensions.Configuration.Consul.Extensions;

public static class ConsulConfigurationExtensions
{
    /// <summary>
    /// Adds a JSON configuration source to <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
    /// <param name="serviceCollection"></param>
    /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
    public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, IServiceCollection serviceCollection)
    {
        serviceCollection.AddConsul();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        //ILogger logger = serviceProvider.GetService<ILogger>() ?? throw new NullReferenceException($"Can't get {nameof(ILogger)} instance from DI Container"); ;
        ILogger logger = Log.ForContext<IConsulConfigurationProvider>();

        IKeyValueClient? kvClient = serviceProvider.GetService<IKeyValueClient>();
        if (kvClient == null)
        {
            logger.Error("Can't get IKeyValueClient instance from DI Container");
            throw new NullReferenceException("Can't get IKeyValueClient instance from DI Container");
        }

        IOptions<ConsulConfigurationOptions>? consulOptions = serviceProvider.GetService<IOptions<ConsulConfigurationOptions>>();
        if (consulOptions == null)
        {
            logger.Error("Can't get IKeyValueClient instance from DI Container, please check your config JSON file");
            throw new NullReferenceException("Can't get IKeyValueClient instance from DI Container, please check your config JSON file");
        }

        var consulConfigSource = new ConsulConfigurationSource(kvClient, consulOptions, logger);

        return builder.Add(consulConfigSource);
    }
}