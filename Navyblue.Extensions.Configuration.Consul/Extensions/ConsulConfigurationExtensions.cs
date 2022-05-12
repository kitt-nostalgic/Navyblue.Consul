using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Navyblue.Consul.KV;
using Navyblue.Extensions.Configuration.Consul.Configs;

namespace Navyblue.Extensions.Configuration.Consul.Extensions;

public static class ConsulConfigurationExtensions
{
    /// <summary>
    /// Adds a JSON configuration source to <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
    /// <param name="serviceProvider"></param>
    /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
    public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, IServiceProvider serviceProvider)
    {
        IKeyValueClient? kvClient = serviceProvider.GetService<IKeyValueClient>();
        if (kvClient == null)
        {
            throw new NullReferenceException("Can't get IKeyValueClient instance from DI Container");
        }

        IOptions<ConsulConfigurationOptions>? consulOptions = serviceProvider.GetService<IOptions<ConsulConfigurationOptions>>();
        if (consulOptions == null)
        {
            throw new NullReferenceException("Can't get IKeyValueClient instance from DI Container, please check your config JSON file");
        }

        var consulConfigSource = new ConsulConfigurationSource(kvClient, consulOptions);

        return builder.Add(consulConfigSource);
    }
}