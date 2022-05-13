using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Navyblue.Consul.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsulController : ControllerBase
    {
        [HttpGet]
        [Route("health")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
