using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaritalStatusesController : ControllerBase
    {
        private IMaritalStatusService _maritalStatusService;

        public MaritalStatusesController(IMaritalStatusService maritalStatusService)
        {
            _maritalStatusService = maritalStatusService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _maritalStatusService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
