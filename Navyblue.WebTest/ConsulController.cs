using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Navyblue.BaseLibrary;

namespace Navyblue.WebTest
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("/api/Consul/")]
    [ApiController]
    public class ConsulController : ControllerBase
    {
        /// <summary>
        /// The consul configuration test
        /// </summary>
        private readonly ConsulConfigTest _consulConfigTest;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsulController"/> class.
        /// </summary>
        /// <param name="test">The test.</param>
        /// <param name="configuration">The configuration.</param>
        public ConsulController(IOptions<ConsulConfigTest> test, IConfiguration configuration)
        {
            _consulConfigTest = test.Value;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            return this.Ok(_consulConfigTest.ToJson());
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        [Route("health")]
        public IActionResult Health()
        {
            return this.Ok(_consulConfigTest.ToJson());
        }
    }
}
