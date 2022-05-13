using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Navyblue.Consul.Extensions.Discovery.Heartbeat;

public class HeartbeatBackgroundService: BackgroundService
{
    private readonly IConsulClient _consulClient;
    private readonly ConsulDiscoveryConfiguration _consulDiscoveryConfiguration;
    private readonly HeartbeatConfiguration _heartbeatConfiguration;

    public HeartbeatBackgroundService(IConsulClient consulClient, 
        IOptions<ConsulDiscoveryConfiguration> consulDiscoveryConfiguration, 
        IOptions<HeartbeatConfiguration> heartbeatConfiguration)
    {
        _consulClient = consulClient;
        _heartbeatConfiguration = heartbeatConfiguration.Value;
        _consulDiscoveryConfiguration = consulDiscoveryConfiguration.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Factory.StartNew(async () =>
        {
            string checkId = _consulDiscoveryConfiguration.ServiceId;

            if (!checkId.StartsWith("service:"))
            {
                checkId = "service:" + checkId;
            }

            checkId = "service:" + checkId;

            // loop until task cancellation is triggered
            while (!stoppingToken.IsCancellationRequested)
            {
                _consulClient.AgentCheckPass(checkId);

                await Task.Delay(TimeSpan.FromMilliseconds(_heartbeatConfiguration.ComputeHeartbeatInterval), stoppingToken);
            }
        }, stoppingToken);
    }
}