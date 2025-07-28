using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace API.Controllers
{
    public class HealthController : ControllerBase
    {

        [HttpGet("/")]
        [HttpGet("/health")]
        public IActionResult GetHealth()
        {
            var healthInfo = new
            {
                status = "UP",
                utcTime = DateTime.UtcNow,
                serverTime = DateTime.Now,
                operatingSystem = RuntimeInformation.OSDescription
            };

            return Ok(healthInfo);
        }
    }
}
