using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Navyblue.BaseLibrary;
using Navyblue.Consul;
using Navyblue.Consul.KV;
using Navyblue.Consul.KV.Model;
using Navyblue.Extensions.Configuration.Consul.Configs;
using Navyblue.Extensions.Configuration.Consul.Extensions;
using Navyblue.Extensions.Configuration.Consul.Parsers;

namespace Navyblue.Extensions.Configuration.Consul
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Configuration.ConfigurationProvider" />
    /// <seealso cref="System.IDisposable" />
    internal sealed class ConsulConfigurationProvider : ConfigurationProvider, IDisposable
    {
        /// <summary>
        /// The cancellation token source
        /// </summary>
        private readonly CancellationTokenSource _cancellationTokenSource;
        /// <summary>
        /// The key value client
        /// </summary>
        private readonly IKeyValueClient _keyValueClient;
        /// <summary>
        /// The consul configuration options
        /// </summary>
        private readonly ConsulConfigurationOptions _consulConfigurationOptions;
        /// <summary>
        /// The last index
        /// </summary>
        private long _lastIndex;
        /// <summary>
        /// The loop refresh
        /// </summary>
        private Task? _loopRefresh;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsulConfigurationProvider"/> class.
        /// </summary>
        /// <param name="keyValueClient">The key value client.</param>
        /// <param name="consulConfigurationOptions">The consul configuration options.</param>
        /// <param name="source">The source.</param>
        public ConsulConfigurationProvider(IKeyValueClient keyValueClient,
            ConsulConfigurationOptions consulConfigurationOptions,
            IConsulConfigurationSource source)
        {
            _consulConfigurationOptions = consulConfigurationOptions;
            _keyValueClient = keyValueClient;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Loads (or reloads) the data for this provider.
        /// </summary>
        public override void Load()
        {
            var cancellationToken = _cancellationTokenSource.Token;

            try
            {
                if (_loopRefresh != null)
                {
                    return;
                }

                var result = GetKvPairs();

                if (result.Value != null)
                {
                    //Data= result.Value.DecodedValue
                    Stream stream = result.Value.DecodedValue.ToStream();
                    Data = JsonParser.ParseStream(stream);

                    SetLastIndex(result.Value.ModifyIndex);
                }

                if (_consulConfigurationOptions.Configuration is { EnabledRefresh: true })
                {
                    _loopRefresh = Task.Run(() => LoopRefresh(cancellationToken), cancellationToken);
                }
            }
            catch (Exception e)
            {
                //TODO 
            }
        }

        /// <summary>
        /// Gets the kv pairs.
        /// </summary>
        /// <returns></returns>
        private ConsulResponse<GetValue> GetKvPairs()
        {
            ConsulResponse<GetValue> result = _keyValueClient.GetKvValue(_consulConfigurationOptions.Configuration.DataKey);

            return result;
        }

        /// <summary>
        /// Loops the refresh.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        private void LoopRefresh(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = GetKvPairs();

                    if (result.Value != null && result.Value.ModifyIndex > _lastIndex)
                    {
                        Stream stream = result.Value.Value.ToStream();
                        Data = JsonParser.ParseStream(stream);

                        SetLastIndex(result.Value.ModifyIndex);
                    }
                }
                catch (Exception)
                {
                    //
                }

                Task.Delay(TimeSpan.FromSeconds(_consulConfigurationOptions.Configuration.RefreshInterval), cancellationToken).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// Sets the last index.
        /// </summary>
        /// <param name="lastIndex">The last index.</param>
        private void SetLastIndex(long lastIndex)
        {
            _lastIndex = lastIndex == 0 ? 1 :
                         lastIndex < _lastIndex ? 0 : lastIndex;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        
    }
}