using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Abstract;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyPlanController : ControllerBase
    {
        private IMonthlyPlanService _monthlyPlanService { get; set; }

        public MonthlyPlanController(IMonthlyPlanService monthlyPlanService)
        {
            _monthlyPlanService = monthlyPlanService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _monthlyPlanService.GetAllByUser();
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _monthlyPlanService.GetById(id);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }        
        [HttpPost("add")]
        public IActionResult Add(MonthlyPlanDto monthlyPlanDto)
        {
            monthlyPlanDto.Id = 0;
            var planExist = _monthlyPlanService.PlanExist(monthlyPlanDto);
            if (!planExist.Success)
                return BadRequest(planExist.Message);

            var result = _monthlyPlanService.Add(monthlyPlanDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("update")]
        public IActionResult Update(MonthlyPlanDto monthlyPlanDto)
        {
            var isOver = _monthlyPlanService.IsOver(monthlyPlanDto.Id);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var planExist = _monthlyPlanService.PlanExist(monthlyPlanDto);
            if (!planExist.Success)
                return BadRequest(planExist.Message);

            var result = _monthlyPlanService.Update(monthlyPlanDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var isOver = _monthlyPlanService.IsOver(id);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _monthlyPlanService.Delete(id);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpGet("getalldetailbyplanid/{planId}")]
        public IActionResult GetAllDetails(int planId)
        {
            var result = _monthlyPlanService.GetAllDetailsByPlanId(planId);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpGet("getdetailbyid/{id}")]
        public IActionResult GetDetailById(int id)
        {
            var result = _monthlyPlanService.GetDetailById(id);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpPost("adddetail")]
        public IActionResult AddDetail(MonthlyPlanDetailDto monthlyPlanDetailDto)
        {
            monthlyPlanDetailDto.Id = 0;
            var isOver = _monthlyPlanService.IsOver(monthlyPlanDetailDto.MonthlyPlanId);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _monthlyPlanService.AddDetail(monthlyPlanDetailDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("updatedetail")]
        public IActionResult UpdateDetail(MonthlyPlanDetailDto monthlyPlanDetailDto)
        {
            var isOver = _monthlyPlanService.IsOver(monthlyPlanDetailDto.MonthlyPlanId);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _monthlyPlanService.UpdateDetail(monthlyPlanDetailDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("deletedetail/{id}")]
        public IActionResult DeleteDetail(int id)
        {
            var planDetail = _monthlyPlanService.GetDetailById(id);
            if (!planDetail.Success)
                return BadRequest(planDetail.Message);

            var isOver = _monthlyPlanService.IsOver(planDetail.Data.MonthlyPlanId);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _monthlyPlanService.Delete(id);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
    }
}
