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
    public class DailyPlanManager : IDailyPlanService, ISecuredUser
    {
        public int UserId { get; set; }
        private IDailyPlanDal _dailyPlanDal { get; set; }
        private IDailyPlanDetailDal _dailyPlanDetailDal { get; set; }
        public DailyPlanManager(IDailyPlanDal dailyPlanDal, IDailyPlanDetailDal dailyPlanDetailDal)
        {
            _dailyPlanDal = dailyPlanDal;
            _dailyPlanDetailDal = dailyPlanDetailDal;
        }
        [SecuredOperation]
        [ValidationAspect(typeof(DailyPlanDtoValidator), Priority = 1)]
        public IResult Add(DailyPlanDto dailyPlanDto)
        {
            _dailyPlanDal.Add(new DailyPlan
            {
                UserId = UserId,
                Description = dailyPlanDto.Description,
                ImportanceTypeId = dailyPlanDto.ImportanceTypeId,
                Date = dailyPlanDto.Date,
                Name = dailyPlanDto.Name
            });

            return new SuccessResult(Messages.DailyPlanAdded);
        }
        [ValidationAspect(typeof(DailyPlanDtoValidator), Priority = 1)]
        public IResult Update(DailyPlanDto dailyPlanDto)
        {
            var plan = _dailyPlanDal.Get(w => w.Id == dailyPlanDto.Id);  

            plan.Description = dailyPlanDto.Description;
            plan.ImportanceTypeId = dailyPlanDto.ImportanceTypeId;
            plan.Date = dailyPlanDto.Date;
            plan.Name = dailyPlanDto.Name;

            _dailyPlanDal.Update(plan);

            return new SuccessResult(Messages.DailyPlanUpdated);
        }

        public IResult Delete(int id)
        {
            var plan = _dailyPlanDal.Get(w => w.Id == id);

            _dailyPlanDal.Delete(plan);

            return new SuccessResult(Messages.DailyPlanDeleted);
        }

        [SecuredOperation]
        public IDataResult<List<DailyPlanView>> GetAllByUser()
        {
            return new SuccessDataResult<List<DailyPlanView>>(_dailyPlanDal.GetListAsView(w => w.UserId == UserId).ToList());
        }

        [SecuredOperation]
        public IDataResult<List<DailyPlanView>> GetAllByWeeklyPlan(WeekInfoDto weekInfoDto)
        {
            
            var firstDateOfWeek = DateTime.Now.FirstDateOfWeek(weekInfoDto.Year, weekInfoDto.Week);
            var lastDateOfWeek = firstDateOfWeek.AddDays(6);

            return new SuccessDataResult<List<DailyPlanView>>(_dailyPlanDal.GetListAsView(w => w.UserId == UserId && firstDateOfWeek <= w.Date && w.Date <= lastDateOfWeek).ToList());
        }

        [SecuredOperation]
        public IDataResult<DailyPlan> GetById(int id)
        {
            var plan = _dailyPlanDal.Get(w => w.Id == id && w.UserId == UserId);
            if (plan == null)
                return new ErrorDataResult<DailyPlan>(Messages.DailyPlanNotFound);

            return new SuccessDataResult<DailyPlan>(plan);
        }

        [SecuredOperation]
        public IResult PlanExist(DailyPlanDto dailyPlanDto)
        {
            var userPlans = _dailyPlanDal.GetList(w => w.UserId.Equals(UserId) && w.Date.Equals(dailyPlanDto.Date) && !w.Id.Equals(dailyPlanDto.Id));
            if (userPlans.Any())
            {
                return new ErrorResult(Messages.DailyPlanExist);
            }

            return new SuccessResult();
        }

        [SecuredOperation]
        public IResult IsOver(int id)
        {
            var plan = _dailyPlanDal.Get(w => w.Id == id && w.UserId == UserId);
            if (plan == null)
                return new ErrorResult(Messages.DailyPlanNotFound);
            else if (plan.IsOver)
                return new ErrorResult(Messages.DailyPlanIsOver);

            return new SuccessResult();
        }

        [SecuredOperation]
        public IDataResult<DailyPlanDetail> GetDetailById(int id)
        {
            var plan = _dailyPlanDetailDal.Get(w => w.Id == id && w.DailyPlan.UserId == UserId);
            if (plan == null)
                return new ErrorDataResult<DailyPlanDetail>(Messages.DailyPlanDetailNotFound);

            return new SuccessDataResult<DailyPlanDetail>(plan);
        }
        [SecuredOperation]
        public IDataResult<List<DailyPlanDetail>> GetAllDetailsByPlanId(int planId)
        {
            return new SuccessDataResult<List<DailyPlanDetail>>(_dailyPlanDetailDal.GetList(w => w.DailyPlan.UserId == UserId && w.DailyPlanId == planId).ToList());
        }

        public IResult AddDetail(DailyPlanDetailDto dailyPlanDetailDto)
        {
            _dailyPlanDetailDal.Add(new DailyPlanDetail
            {
                DailyPlanId = dailyPlanDetailDto.DailyPlanId,
                Description = dailyPlanDetailDto.Description
            });

            return new SuccessResult(Messages.DailyPlanDetailAdded);
        }

        public IResult UpdateDetail(DailyPlanDetailDto dailyPlanDetailDto)
        {
            var planDetail = _dailyPlanDetailDal.Get(w => w.Id == dailyPlanDetailDto.Id && w.DailyPlanId == dailyPlanDetailDto.DailyPlanId);

            planDetail.Description = dailyPlanDetailDto.Description;

            _dailyPlanDetailDal.Update(planDetail);

            return new SuccessResult(Messages.DailyPlanDetailUpdated);
        }

        public IResult DeleteDetail(int id)
        {
            var planDetail = _dailyPlanDetailDal.Get(w => w.Id == id);

            _dailyPlanDetailDal.Delete(planDetail);

            return new SuccessResult(Messages.DailyPlanDetailDeleted);
        }

       
    }
}