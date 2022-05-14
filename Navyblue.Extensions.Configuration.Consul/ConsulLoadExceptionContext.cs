using Microsoft.Extensions.Configuration;

namespace Navyblue.Extensions.Configuration.Consul
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class ConsulLoadExceptionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsulLoadExceptionContext"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="exception">The exception.</param>
        internal ConsulLoadExceptionContext(IConfigurationSource source, Exception exception)
        {
            Source = source;
            Exception = exception;
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public IConfigurationSource Source { get; }
    }
}