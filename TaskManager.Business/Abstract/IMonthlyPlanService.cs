using System.Collections.Generic;
using TaskManager.Core.Utilities.Results;
using TaskManager.Entities.Concrete;
using TaskManager.Entities.Concrete.Dtos;
using TaskManager.Entities.Concrete.View;

namespace TaskManager.Business.Abstract
{
    public interface IMonthlyPlanService
    {
        IDataResult<MonthlyPlan> GetById(int id);
        IDataResult<List<MonthlyPlanView>> GetAllByUser();
        IResult Add(MonthlyPlanDto monthlyPlanDto);
        IResult Update(MonthlyPlanDto monthlyPlanDto);
        IResult Delete(int id);
        IDataResult<MonthlyPlanDetail> GetDetailById(int id);
        IDataResult<List<MonthlyPlanDetail>> GetAllDetailsByPlanId(int planId);
        IResult AddDetail(MonthlyPlanDetailDto monthlyPlanDetailDto);
        IResult UpdateDetail(MonthlyPlanDetailDto monthlyPlanDetailDto);
        IResult DeleteDetail(int id);
        IResult PlanExist(MonthlyPlanDto monthlyPlanDto);
        IResult IsOver(int id);
    }
}
