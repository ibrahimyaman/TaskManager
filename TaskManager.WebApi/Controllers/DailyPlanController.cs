using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Abstract;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyPlanController : ControllerBase
    {
        private IDailyPlanService _dailyPlanService { get; set; }

        public DailyPlanController(IDailyPlanService dailyPlanService)
        {
            _dailyPlanService = dailyPlanService; 
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _dailyPlanService.GetAllByUser();
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _dailyPlanService.GetById(id);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpGet("getbyweeklyplan")]
        public IActionResult GetByWeeklyPlanId(WeekInfoDto weekInfoDto)
        {
            var result = _dailyPlanService.GetAllByWeeklyPlan(weekInfoDto);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(DailyPlanDto dailyPlanDto)
        {
            dailyPlanDto.Id = 0;
            var planExist = _dailyPlanService.PlanExist(dailyPlanDto);
            if (!planExist.Success)
                return BadRequest(planExist.Message);

            var result = _dailyPlanService.Add(dailyPlanDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("update")]
        public IActionResult Update(DailyPlanDto dailyPlanDto)
        {
            var isOver = _dailyPlanService.IsOver(dailyPlanDto.Id);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var planExist = _dailyPlanService.PlanExist(dailyPlanDto);
            if (!planExist.Success)
                return BadRequest(planExist.Message);

            var result = _dailyPlanService.Update(dailyPlanDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var isOver = _dailyPlanService.IsOver(id);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _dailyPlanService.Delete(id);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [HttpGet("getalldetailbyplanid/{planId}")]
        public IActionResult GetAllDetails(int planId)
        {
            var result = _dailyPlanService.GetAllDetailsByPlanId(planId);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpGet("getdetailbyid/{id}")]
        public IActionResult GetDetailById(int id)
        {
            var result = _dailyPlanService.GetDetailById(id);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
        [HttpPost("adddetail")]
        public IActionResult AddDetail(DailyPlanDetailDto dailyPlanDto)
        {
            dailyPlanDto.Id = 0;
            var isOver = _dailyPlanService.IsOver(dailyPlanDto.DailyPlanId);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _dailyPlanService.AddDetail(dailyPlanDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("updatedetail")]
        public IActionResult UpdateDetail(DailyPlanDetailDto dailyPlanDto)
        {
            var isOver = _dailyPlanService.IsOver(dailyPlanDto.DailyPlanId);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _dailyPlanService.UpdateDetail(dailyPlanDto);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPost("deletedetail/{id}")]
        public IActionResult DeleteDetail(int id)
        {
            var planDetail = _dailyPlanService.GetDetailById(id);
            if (!planDetail.Success)
                return BadRequest(planDetail.Message);

            var isOver = _dailyPlanService.IsOver(planDetail.Data.DailyPlanId);
            if (!isOver.Success)
                return BadRequest(isOver.Message);

            var result = _dailyPlanService.Delete(id);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
    }
}
