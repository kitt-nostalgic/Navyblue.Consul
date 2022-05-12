using Navyblue.Consul.Agent.Model;

namespace Navyblue.Consul.Extensions.Discovery.ServiceRegistry;

/// <summary>
///
/// </summary>
public interface IConsulServiceRegistry
{
    /// <summary>
    /// Gets the service.
    /// </summary>
    public NewService Service { get; }

    /// <summary>
    /// Registrations this instance.
    /// </summary>
    /// <returns></returns>
    ConsulServiceRegistry Registration();

    /// <summary>
    /// Sets the check.
    /// </summary>
    /// <param name="service">The service.</param>
    void SetCheck(NewService service);

    /// <summary>
    /// Gets the instance identifier.
    /// </summary>
    /// <returns></returns>
    string? GetInstanceId();

    /// <summary>
    /// Creates the tags.
    /// </summary>
    /// <returns></returns>
    List<string> CreateTags();

    /// <summary>
    /// Creates the check.
    /// </summary>
    /// <param name="port">The port.</param>
    /// <returns></returns>
    NewService.Check CreateCheck(int port);

    /// <summary>
    /// Gets the name of the application.
    /// </summary>
    /// <returns></returns>
    string? GetAppName();

    /// <summary>
    /// Initializes the port.
    /// </summary>
    /// <param name="knownPort">The known port.</param>
    void InitializePort(int knownPort);

    /// <summary>
    /// Gets the service identifier.
    /// </summary>
    /// <returns></returns>
    string? GetServiceId();

    /// <summary>
    /// Gets the host.
    /// </summary>
    /// <returns></returns>
    string? GetHost();

    /// <summary>
    /// Gets the port.
    /// </summary>
    /// <returns></returns>
    int GetPort();

    /// <summary>
    /// Determines whether this instance is secure.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is secure; otherwise, <c>false</c>.
    /// </returns>
    bool IsSecure();

    /// <summary>
    /// Gets the metadata.
    /// </summary>
    /// <returns></returns>
    Dictionary<string, string> GetMetadata();

    /// <summary>
    /// Gets the URI.
    /// </summary>
    /// <returns></returns>
    Uri GetUri();
}