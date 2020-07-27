using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Abstract;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeklyPlanController : ControllerBase
    {
        private IWeeklyPlanService _weeklyPlanService { get; set; }

        public WeeklyPlanController(IWeeklyPlanService weeklyPlanService)
        {
            _weeklyPlanService = weeklyPlanService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _weeklyPlanService.GetAllByUser();
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _weeklyPlanService.GetById(id);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpGet("getbymonthlyplan")]
        public IActionResult GetByMonthlyPlanId(MonthInfoDto monthInfoDto)
        {
            var result = _weeklyPlanService.GetAllByMonthlyPlan(monthInfoDto);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(WeeklyPlanDto weeklyPlanDto)
        {
            weeklyPlanDto.Id = 0;
            var planExist = _weeklyPlanService.PlanExist(weeklyPlanDto);
            if (!planExist.Success)
                return BadRequest(planExist.Message);

            var result = _weeklyPlanService.Add(weeklyPlanDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("update")]
        public IActionResult Update(WeeklyPlanDto weeklyPlanDto)
        {
            var isOver = _weeklyPlanService.IsOver(weeklyPlanDto.Id);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var planExist = _weeklyPlanService.PlanExist(weeklyPlanDto);
            if (!planExist.Success)
                return BadRequest(planExist.Message);

            var result = _weeklyPlanService.Update(weeklyPlanDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var isOver = _weeklyPlanService.IsOver(id);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _weeklyPlanService.Delete(id);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpGet("getalldetailbyplanid/{planId}")]
        public IActionResult GetAllDetails(int planId)
        {
            var result = _weeklyPlanService.GetAllDetailsByPlanId(planId);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpGet("getdetailbyid/{id}")]
        public IActionResult GetDetailById(int id)
        {
            var result = _weeklyPlanService.GetDetailById(id);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpPost("adddetail")]
        public IActionResult AddDetail(WeeklyPlanDetailDto weeklyPlanDto)
        {
            weeklyPlanDto.Id = 0;
            var isOver = _weeklyPlanService.IsOver(weeklyPlanDto.WeeklyPlanId);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _weeklyPlanService.AddDetail(weeklyPlanDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("updatedetail")]
        public IActionResult UpdateDetail(WeeklyPlanDetailDto weeklyPlanDto)
        {
            var isOver = _weeklyPlanService.IsOver(weeklyPlanDto.WeeklyPlanId);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _weeklyPlanService.UpdateDetail(weeklyPlanDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("deletedetail/{id}")]
        public IActionResult DeleteDetail(int id)
        {
            var planDetail = _weeklyPlanService.GetDetailById(id);
            if (!planDetail.Success)
                return BadRequest(planDetail.Message);

            var isOver = _weeklyPlanService.IsOver(planDetail.Data.WeeklyPlanId);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _weeklyPlanService.Delete(id);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
    }
}
