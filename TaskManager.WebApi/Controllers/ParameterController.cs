using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Abstract;

namespace TaskManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private IImportanceTypeService _importanceTypeService { get; set; }
        public ParameterController(IImportanceTypeService importanceTypeService)
        {
            _importanceTypeService = importanceTypeService;
        }

        [HttpGet("getimportancetypes")]
        public IActionResult GetImportanceTypes()
        {
            var result = _importanceTypeService.GetAll();
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
    }
}
