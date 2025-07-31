using Application.Workshops.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ExternalServicesController : ApiController
    {
        private readonly IWorkshopService _workshopService;

        public ExternalServicesController(IWorkshopService workshopService)
        {
            _workshopService = workshopService;
        }


        [HttpGet("Workshop")]
        public async Task<IActionResult> Get()
        {
            var workshops = await _workshopService.GetActiveWorkshopsAsync();

            int id = workshops.Select(x => x.Id).FirstOrDefault();

            bool workshopExists = await _workshopService.ExistsAsync(id);

            return Ok("Workshop is Ok");
        }
    }
}
