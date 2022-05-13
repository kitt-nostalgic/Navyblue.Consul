using System.ComponentModel.DataAnnotations;
using Navyblue.BaseLibrary;
using Newtonsoft.Json;
using Serilog;

namespace Navyblue.Consul.Extensions.Discovery.Heartbeat;

/// <summary>
///   <br />
/// </summary>
public class HeartbeatConfiguration
{
    /// <summary>
    /// The log
    /// </summary>
    private readonly ILogger _log = new LoggerConfiguration().WriteTo.File("").CreateLogger();

    /// <summary>
    /// </summary>
    /// <value>
    ///   <c>true</c> if enabled; otherwise, <c>false</c>.
    /// </value>
    public bool Enabled { get; set; }= false;

    /// <summary>
    /// Gets or sets the TTL value.
    /// </summary>
    /// <value>
    /// The TTL value.
    /// </value>
    [Range(minimum: 1, int.MaxValue)]
    public int TtlValue { get; set; } = 30;

    /// <summary>
    /// Gets or sets the TTL unit.
    /// </summary>
    /// <value>
    /// The TTL unit.
    /// </value>
    [JsonRequired]
    public string TtlUnit { get; set; } = "s";

    /// <summary>
    /// Gets or sets the interval ratio.
    /// </summary>
    /// <value>
    /// The interval ratio.
    /// </value>
    [Range(minimum: 0.1, 0.9)]
    public double IntervalRatio { get; set; } = 2.0 / 3.0;

    /// <summary>
    /// Computes the heartbeat interval.
    /// </summary>
    /// <returns></returns>
    public double ComputeHeartbeatInterval
    {
        get
        {
            // heartbeat rate at ratio * ttl, but no later than ttl -1s and, (under lesser priority), no sooner than 1s from now
            double interval = this.TtlValue * this.IntervalRatio;
            double max = Math.Max(interval, 1);
            int ttlMinus1 = this.TtlValue - 1;
            double min = Math.Max(ttlMinus1, max);
            _log.Debug("Computed heartbeatInterval: " + Math.Round(1000 * min));
            return Math.Round(1000 * min);
        }
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A string that represents the current object.
    /// </returns>
    public override String ToString()
    {
        return this.ToJson();
    }

}