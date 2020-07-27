using System.Collections.Generic;
using TaskManager.Core.Utilities.Results;
using TaskManager.Entities.Concrete;
using TaskManager.Entities.Concrete.Dtos;
using TaskManager.Entities.Concrete.View;

namespace TaskManager.Business.Abstract
{
    public interface IDailyPlanService
    {
        IDataResult<DailyPlan> GetById(int id);
        IDataResult<List<DailyPlanView>> GetAllByUser();
        IDataResult<List<DailyPlanView>> GetAllByWeeklyPlan(WeekInfoDto weekInfoDto);
        IResult Add(DailyPlanDto dailyPlanDto);
        IResult Update(DailyPlanDto dailyPlanDto);
        IResult Delete(int id);
        IDataResult<DailyPlanDetail> GetDetailById(int id);
        IDataResult<List<DailyPlanDetail>> GetAllDetailsByPlanId(int planId);
        IResult AddDetail(DailyPlanDetailDto dailyPlanDetailDto);
        IResult UpdateDetail(DailyPlanDetailDto dailyPlanDetailDto);
        IResult DeleteDetail(int id);
        IResult IsOver(int id);
        IResult PlanExist(DailyPlanDto dailyPlanDto);
    }
}
