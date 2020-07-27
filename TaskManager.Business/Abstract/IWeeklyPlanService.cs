using System.Collections.Generic;
using TaskManager.Core.Utilities.Results;
using TaskManager.Entities.Concrete;
using TaskManager.Entities.Concrete.Dtos;
using TaskManager.Entities.Concrete.View;

namespace TaskManager.Business.Abstract
{
    public interface IWeeklyPlanService
    {
        IDataResult<WeeklyPlan> GetById(int id);
        IDataResult<List<WeeklyPlanView>> GetAllByUser();
        IDataResult<List<WeeklyPlanView>> GetAllByMonthlyPlan(MonthInfoDto monthInfoDto);
        IResult Add(WeeklyPlanDto weeklyPlanDto);
        IResult Update(WeeklyPlanDto weeklyPlanDto);
        IResult Delete(int id);
        IDataResult<WeeklyPlanDetail> GetDetailById(int id);
        IDataResult<List<WeeklyPlanDetail>> GetAllDetailsByPlanId(int planId);
        IResult AddDetail(WeeklyPlanDetailDto weeklyPlanDetailDto);
        IResult UpdateDetail(WeeklyPlanDetailDto weeklyPlanDetailDto);
        IResult DeleteDetail(int id);
        IResult PlanExist(WeeklyPlanDto weeklyPlanDto);
        IResult IsOver(int id);
    }
}
