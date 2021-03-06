using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Configuration;
using Navyblue.BaseLibrary;

namespace Navyblue.Consul.Extensions.Discovery
{
    /// <summary>
    ///
    /// </summary>
    public class ConsulDiscoveryConfiguration
    {
        private readonly IConfiguration _configuration;

        public ConsulDiscoveryConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets or sets the acl token.
        /// </summary>
        /// <value>
        /// The acl token.
        /// </value>
        public string? AclToken { get; set; }

        /// <summary>
        /// Tags to use when registering service.
        /// </summary>
        public List<string> Tags = new();

        /// <summary>
        /// Is service discovery enabled?
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Alternate server path to invoke for health checking
        /// </summary>
        /// <value>
        /// The health check path.
        /// </value>
        public string HealthCheckPath { get; set; } = "/actuator/health";

        /// <summary>
        /// Headers to be applied to the Health Check calls.
        /// </summary>
        /// <value>
        /// The health check headers.
        /// </value>
        public IDictionary<string, IList<string>>? HealthCheckHeaders { get; set; } =
            new Dictionary<string, IList<string>>();

        /// <summary>
        /// How often to perform the health check (e.g. 10s), defaults to 10s
        /// </summary>
        /// <value>
        /// The health check interval.
        /// </value>
        public string HealthCheckInterval { get; set; } = "10s";

        /// <summary>
        /// Timeout for health check (e.g. 10s)
        /// </summary>
        /// <value>
        /// The health check timeout.
        /// </value>
        public string? HealthCheckTimeout { get; set; }

        /// <summary>
        /// Timeout to deregister services critical for longer than timeout (e.g. 30m).
        /// Requires consul version 7.x or higher.
        /// </summary>
        /// <value>
        /// The health check critical timeout.
        /// </value>
        public string? HealthCheckCriticalTimeout { get; set; }

        /// <summary>
        /// IP address to use when accessing service (must also set preferIpAddress to use).
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        public string IpAddress
        {
            get
            {
#if DEBUG
                return "localhost";
#endif

                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("www.baidu.com", 80);
                    IPEndPoint? endPoint = socket.LocalEndPoint as IPEndPoint;
                    if (endPoint == null)
                        throw new NullReferenceException("Can't get IPEndPoint");

                    return endPoint.Address.ToString();
                }
            }
        }

        /// <summary>
        /// Hostname to use when accessing server
        /// </summary>
        /// <value>
        /// The hostname.
        /// </value>
        public string? Hostname { get; set; }


        private int? _port;

        /// <summary>
        /// Port to register the service under (defaults to listening port)
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port
        {
            get
            {
                if (_port != null) return _port.Value;

                var currentUrl = _configuration.GetSection("ASPNETCORE_URLS").Value;
                var httpScheme = _configuration.GetSection("Consul:Discovery:Scheme").Value;

                string[] urls = currentUrl.Split(";");
                string scheme = httpScheme.IsNullOrWhiteSpace() ? this.Scheme : httpScheme;

                foreach (var url in urls)
                {
                    string[] urlParts = url.Split(":");
                    if (urlParts[0] == scheme)
                    {
                        this._port = Convert.ToInt32(urlParts[2]);
                        break;
                    }
                }

                return _port.GetValueOrDefault();
            }
            set => this._port = value;
        }

        /// <summary>
        /// Use ip address rather than hostname during registration.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is prefer ip address; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreferIpAddress { get; set; } = false;

        /// <summary>
        /// Source of how we will determine the address to use
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is prefer agent address; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreferAgentAddress { get; set; } = false;

        /// <summary>
        /// The delay between calls to watch consul catalog in millis, default is 1000.
        /// </summary>
        /// <value>
        /// The catalog services watch delay.
        /// </value>
        public int CatalogServicesWatchDelay { get; set; } = 1000;

        /// <summary>
        /// The number of seconds to block while watching consul catalog, default is 2.
        /// </summary>
        /// <value>
        /// The catalog services watch timeout.
        /// </value>
        public int CatalogServicesWatchTimeout { get; set; } = 2;

        /// <summary>
        /// The service name
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string? ServiceName { get; set; }

        /// <summary>
        /// The instance identifier
        /// </summary>
        /// <value>
        /// The instance identifier.
        /// </value>
        public string InstanceId { get; set; }

        /// <summary>
        /// Service instance group.
        /// </summary>
        /// <value>
        /// The instance group.
        /// </value>
        public string? InstanceGroup { get; set; }

        private string _scheme = "http";

        /// <summary>
        /// Whether to register an http or https service
        /// </summary>
        /// <value>
        /// The scheme.
        /// </value>
        public string Scheme {
            get
            {
                var currentUrl = _configuration.GetSection("ASPNETCORE_URLS").Value;

                string? url = currentUrl.Split(";").FirstOrDefault();
                if (url.IsNotNullOrWhiteSpace())
                {
                    string[] urlParts = url.Split(":");
                    this._scheme = urlParts[0];
                }

                return _scheme;
            }
        }

        /// <summary>
        /// Map of serviceId's -&gt; tag to query for in server list. This allows filtering services by a single tag.
        /// </summary>
        /// <value>
        /// The server list query tags.
        /// </value>
        public Dictionary<string, string> ServerListQueryTags { get; set; } = new();

        /// <summary>
        /// Map of serviceId's -&gt; datacenter to query for in server list. This allows looking up services in another datacenters.
        /// </summary>
        /// <value>
        /// The datacenters.
        /// </value>
        public Dictionary<string, string> Datacenters { get; set; } = new();

        /** Tag to query for in service list if one is not listed in serverListQueryTags. */
        /// <summary>
        /// Gets or sets the default query tag.
        /// </summary>
        /// <value>
        /// The default query tag.
        /// </value>
        public string? DefaultQueryTag { get; set; }

        /// <summary>
        /// Add the 'passing` parameter to /v1/health/service/serviceName. This pushes health check passing to the server.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is query passing; otherwise, <c>false</c>.
        /// </value>
        public bool IsQueryPassing { get; set; } = false;

        /// <summary>
        /// Register as a service in consul
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is register; otherwise, <c>false</c>.
        /// </value>
        public bool IsRegister { get; set; } = true;

        /// <summary>
        /// Disable automatic de-registration of service in consul
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deregister; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeregister { get; set; } = true;

        /// <summary>
        /// Register health check in consul. Useful during development of a service.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is register health check; otherwise, <c>false</c>.
        /// </value>
        public bool IsRegisterHealthCheck { get; set; } = true;

        /// <summary>
        /// Throw exceptions during service registration if true, otherwise, log warnings (defaults to true).
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is fail fast; otherwise, <c>false</c>.
        /// </value>
        public bool IsFailFast { get; set; } = true;

        /// <summary>
        /// Gets the query tag for service.
        /// </summary>
        /// <param name="serviceId">The service identifier.</param>
        /// <returns></returns>
        public string GetQueryTagForService(string serviceId)
        {
            string tag = this.ServerListQueryTags[serviceId];
            return tag;
        }

        /// <summary>
        /// Gets the hostname.
        /// </summary>
        /// <returns></returns>
        public string? GetHostname()
        {
            return this.IsPreferIpAddress ? this.IpAddress : this.Hostname;
        }

        public string ServiceId => this.InstanceId.IsNotNullOrEmpty() ? $"{this.ServiceName}_{this.IpAddress}:{this.Port}" : this.InstanceId.FormatWith(this.IpAddress);
    }
}