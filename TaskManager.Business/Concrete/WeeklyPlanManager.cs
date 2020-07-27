using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Business.Abstract;
using TaskManager.Business.BusinessAspect;
using TaskManager.Business.Constants;
using TaskManager.Business.ValidationRules.FluentValidation;
using TaskManager.Core.Aspects.Autofac.Validation;
using TaskManager.Core.Extensions;
using TaskManager.Core.Utilities.Results;
using TaskManager.DataAccess.Abstract;
using TaskManager.Entities.Concrete;
using TaskManager.Entities.Concrete.Dtos;
using TaskManager.Entities.Concrete.View;

namespace TaskManager.Business.Concrete
{
    public class WeeklyPlanManager : IWeeklyPlanService, ISecuredUser
    {
        public int UserId { get; set; }
        private IWeeklyPlanDal _weeklyPlanDal { get; set; }
        private IWeeklyPlanDetailDal _weeklyPlanDetailDal { get; set; }
        private IMonthlyPlanDal _monthlyPlanDal { get; set; }
        public WeeklyPlanManager(IWeeklyPlanDal weeklyPlanDal, 
            IMonthlyPlanDal monthlyPlanDal,
            IWeeklyPlanDetailDal eeklyPlanDetailDal)
        {
            _weeklyPlanDal = weeklyPlanDal;
            _monthlyPlanDal = monthlyPlanDal;
            _weeklyPlanDetailDal = eeklyPlanDetailDal;
        }
        [SecuredOperation]
        [ValidationAspect(typeof(WeeklyPlanDtoValidator), Priority = 1)]
        public IResult Add(WeeklyPlanDto weeklyPlanDto)
        {
            _weeklyPlanDal.Add(new WeeklyPlan
            {
                UserId = UserId,
                Description = weeklyPlanDto.Description,
                ImportanceTypeId = weeklyPlanDto.ImportanceTypeId,
                WeekNumber = weeklyPlanDto.WeekNumber,
                Name = weeklyPlanDto.Name,
                Year = weeklyPlanDto.Year,
            });

            return new SuccessResult(Messages.WeeklyPlanAdded);
        }

        [ValidationAspect(typeof(MonthlyPlanDtoValidator), Priority = 1)]
        public IResult Update(WeeklyPlanDto weeklyPlanDto)
        {
            var plan = _weeklyPlanDal.Get(w => w.Id == weeklyPlanDto.Id);           

            plan.Description = weeklyPlanDto.Description;
            plan.ImportanceTypeId = weeklyPlanDto.ImportanceTypeId;
            plan.WeekNumber = weeklyPlanDto.WeekNumber;
            plan.Name = weeklyPlanDto.Name;
            plan.Year = weeklyPlanDto.Year;

            _weeklyPlanDal.Update(plan);

            return new SuccessResult(Messages.WeeklyPlanUpdated);
        }

        public IResult Delete(int id)
        {
            var plan = _weeklyPlanDal.Get(w => w.Id == id);

            _weeklyPlanDal.Delete(plan);

            return new SuccessResult(Messages.WeeklyPlanDeleted);
        }
        [SecuredOperation]
        public IDataResult<List<WeeklyPlanView>> GetAllByMonthlyPlan(MonthInfoDto monthInfoDto)
        {
            var firstDateOfMonth = new DateTime(monthInfoDto.Year, monthInfoDto.Month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

            var firstWeek = firstDateOfMonth.Week();
            var lastWeek = lastDateOfMonth.Week();

            return new SuccessDataResult<List<WeeklyPlanView>>(_weeklyPlanDal.GetListAsView(w => w.UserId == UserId && w.Year == monthInfoDto.Year && firstWeek <= w.WeekNumber && w.WeekNumber <= lastWeek).ToList());
        }
        [SecuredOperation]
        public IDataResult<List<WeeklyPlanView>> GetAllByUser()
        {
            return new SuccessDataResult<List<WeeklyPlanView>>(_weeklyPlanDal.GetListAsView(w => w.UserId == UserId).ToList());
        }

        [SecuredOperation]
        public IDataResult<WeeklyPlan> GetById(int id)
        {
            var plan = _weeklyPlanDal.Get(w => w.Id == id && w.UserId == UserId);
            if (plan == null)
                return new ErrorDataResult<WeeklyPlan>(Messages.WeeklyPlanNotFound);

            return new SuccessDataResult<WeeklyPlan>(plan);
        }


        public IResult MonthlyPlanIsAppropriate(WeeklyPlanDto weeklyPlanDto)
        {
            var monthlyPlan = _monthlyPlanDal.Get(w => w.Id == weeklyPlanDto.MonthlyPlanId);
            if (monthlyPlan != null)
            {
                var firstDateOfWeek = DateTime.Today.FirstDateOfWeek(weeklyPlanDto.Year, weeklyPlanDto.WeekNumber);
                var lastDateOfWeek = firstDateOfWeek.AddDays(6);
                var thursdayDateOfWeek = firstDateOfWeek.AddDays(3);

                var firstDateOfMonth = new DateTime(monthlyPlan.Year, monthlyPlan.Month, 1);
                var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

                if (!(firstDateOfWeek <= firstDateOfMonth && firstDateOfMonth <= thursdayDateOfWeek) &&
                     !(thursdayDateOfWeek >= lastDateOfMonth && lastDateOfMonth >= lastDateOfWeek))
                {
                    return new ErrorResult(Messages.WeeklyPlanNotAppropriate);
                }
            }

            return new SuccessResult();
        }
        [SecuredOperation]
        public IResult PlanExist(WeeklyPlanDto weeklyPlanDto)
        {
            var userPlans = _weeklyPlanDal.GetList(w => w.UserId.Equals(UserId) && w.Year.Equals(weeklyPlanDto.Year) && w.WeekNumber.Equals(weeklyPlanDto.WeekNumber) && !w.Id.Equals(weeklyPlanDto.Id));
            if (userPlans.Any())
            {
                return new ErrorResult(Messages.WeeklyPlanExist);
            }

            return new SuccessResult();
        }
        [SecuredOperation]
        public IResult IsOver(int id)
        {
            var plan = _weeklyPlanDal.Get(w => w.Id == id && w.UserId == UserId);
            if (plan == null)
                return new ErrorResult(Messages.WeeklyPlanNotFound);
            else if (plan.IsOver)
                return new ErrorResult(Messages.WeeklyPlanIsOver);

            return new SuccessResult();
        }

        [SecuredOperation]
        public IDataResult<WeeklyPlanDetail> GetDetailById(int id)
        {
            var plan = _weeklyPlanDetailDal.Get(w => w.Id == id && w.WeeklyPlan.UserId == UserId);
            if (plan == null)
                return new ErrorDataResult<WeeklyPlanDetail>(Messages.DailyPlanDetailNotFound);

            return new SuccessDataResult<WeeklyPlanDetail>(plan);
        }
        [SecuredOperation]
        public IDataResult<List<WeeklyPlanDetail>> GetAllDetailsByPlanId(int planId)
        {
            return new SuccessDataResult<List<WeeklyPlanDetail>>(_weeklyPlanDetailDal.GetList(w => w.WeeklyPlan.UserId == UserId && w.WeeklyPlanId == planId).ToList());
        }

        public IResult AddDetail(WeeklyPlanDetailDto weeklyPlanDetailDto)
        {
            _weeklyPlanDetailDal.Add(new WeeklyPlanDetail
            {
                WeeklyPlanId = weeklyPlanDetailDto.WeeklyPlanId,
                Description = weeklyPlanDetailDto.Description
            });

            return new SuccessResult(Messages.WeeklyPlanDetailAdded);
        }

        public IResult UpdateDetail(WeeklyPlanDetailDto weeklyPlanDetailDto)
        {
            var planDetail = _weeklyPlanDetailDal.Get(w => w.Id == weeklyPlanDetailDto.Id && w.WeeklyPlanId == weeklyPlanDetailDto.WeeklyPlanId);

            planDetail.Description = weeklyPlanDetailDto.Description;

            _weeklyPlanDetailDal.Update(planDetail);

            return new SuccessResult(Messages.WeeklyPlanDetailUpdated);
        }

        public IResult DeleteDetail(int id)
        {
            var planDetail = _weeklyPlanDetailDal.Get(w => w.Id == id);

            _weeklyPlanDetailDal.Delete(planDetail);

            return new SuccessResult(Messages.DailyPlanDetailDeleted);
        }
    }
}
