using System.Collections.Generic;
using System.Linq;
using TaskManager.Business.Abstract;
using TaskManager.Business.BusinessAspect;
using TaskManager.Business.Constants;
using TaskManager.Business.ValidationRules.FluentValidation;
using TaskManager.Core.Aspects.Autofac.Validation;
using TaskManager.Core.Utilities.Results;
using TaskManager.DataAccess.Abstract;
using TaskManager.Entities.Concrete;
using TaskManager.Entities.Concrete.Dtos;
using TaskManager.Entities.Concrete.View;

namespace TaskManager.Business.Concrete
{
    public class MonthlyPlanManager : IMonthlyPlanService, ISecuredUser
    {
        public int UserId { get; set; }
        private IMonthlyPlanDal _monthlyPlanDal { get; set; }
        private IMonthlyPlanDetailDal _monthlyPlanDetailDal { get; set; }

        public MonthlyPlanManager(IMonthlyPlanDal monthlyPlanDal,
            IMonthlyPlanDetailDal monthlyPlanDetailDal)
        {
            _monthlyPlanDal = monthlyPlanDal;
            _monthlyPlanDetailDal = monthlyPlanDetailDal;
        }

        [SecuredOperation]
        public IDataResult<MonthlyPlan> GetById(int id)
        {
            var plan = _monthlyPlanDal.Get(w => w.Id == id && w.UserId == UserId);
            if (plan == null)
                return new ErrorDataResult<MonthlyPlan>(Messages.MonthlyPlanNotFound);
            return new SuccessDataResult<MonthlyPlan>(plan);
        }
        [SecuredOperation]
        public IDataResult<List<MonthlyPlanView>> GetAllByUser()
        {
            return new SuccessDataResult<List<MonthlyPlanView>>(_monthlyPlanDal.GetListAsView(w => w.UserId == UserId).ToList());
        }
        [SecuredOperation]
        [ValidationAspect(typeof(MonthlyPlanDtoValidator), Priority = 1)]
        public IResult Add(MonthlyPlanDto monthlyPlanDto)
        {
            _monthlyPlanDal.Add(new MonthlyPlan
            {
                UserId = UserId,
                Description = monthlyPlanDto.Description,
                ImportanceTypeId = monthlyPlanDto.ImportanceTypeId,
                Month = monthlyPlanDto.Month,
                Name = monthlyPlanDto.Name,
                Year = monthlyPlanDto.Year
            });

            return new SuccessResult(Messages.MonthlyPlanAdded);
        }

        [ValidationAspect(typeof(MonthlyPlanDtoValidator), Priority = 1)]
        public IResult Update(MonthlyPlanDto monthlyPlanDto)
        {
            var plan = _monthlyPlanDal.Get(w => w.Id == monthlyPlanDto.Id);

            plan.Description = monthlyPlanDto.Description;
            plan.ImportanceTypeId = monthlyPlanDto.ImportanceTypeId;
            plan.Month = monthlyPlanDto.Month;
            plan.Name = monthlyPlanDto.Name;
            plan.Year = monthlyPlanDto.Year;

            _monthlyPlanDal.Update(plan);

            return new SuccessResult(Messages.MonthlyPlanUpdated);
        }
        public IResult Delete(int id)
        {
            var plan = _monthlyPlanDal.Get(w => w.Id == id);

            _monthlyPlanDal.Delete(plan);

            return new SuccessResult(Messages.MonthlyPlanDeleted);
        }

        [SecuredOperation]
        public IResult PlanExist(MonthlyPlanDto monthlyPlanDto)
        {
            var userPlans = _monthlyPlanDal.GetList(w => w.UserId.Equals(UserId) && w.Year.Equals(monthlyPlanDto.Year) && w.Month.Equals(monthlyPlanDto.Month) && !w.Id.Equals(monthlyPlanDto.Id));
            if (userPlans.Any())
            {
                return new ErrorResult(Messages.MonthlyPlanExist);
            }

            return new SuccessResult();
        }

        [SecuredOperation]
        public IResult IsOver(int id)
        {
            var plan = _monthlyPlanDal.Get(w => w.Id == id && w.UserId == UserId);
            if (plan == null)
                return new ErrorResult(Messages.MonthlyPlanNotFound);
            else if (plan.IsOver)
                return new ErrorResult(Messages.MonthlyPlanIsOver);

            return new SuccessResult();
        }
        [SecuredOperation]
        public IDataResult<MonthlyPlanDetail> GetDetailById(int id)
        {
            var plan = _monthlyPlanDetailDal.Get(w => w.Id == id && w.MonthlyPlan.UserId == UserId);
            if (plan == null)
                return new ErrorDataResult<MonthlyPlanDetail>(Messages.DailyPlanDetailNotFound);

            return new SuccessDataResult<MonthlyPlanDetail>(plan);
        }
        [SecuredOperation]
        public IDataResult<List<MonthlyPlanDetail>> GetAllDetailsByPlanId(int planId)
        {
            return new SuccessDataResult<List<MonthlyPlanDetail>>(_monthlyPlanDetailDal.GetList(w => w.MonthlyPlan.UserId == UserId && w.MonthlyPlanId == planId).ToList());
        }

        public IResult AddDetail(MonthlyPlanDetailDto monthlyPlanDetailDto)
        {
            _monthlyPlanDetailDal.Add(new MonthlyPlanDetail
            {
                MonthlyPlanId = monthlyPlanDetailDto.MonthlyPlanId,
                Description = monthlyPlanDetailDto.Description
            });

            return new SuccessResult(Messages.MonthlyPlanDetailAdded);
        }

        public IResult UpdateDetail(MonthlyPlanDetailDto monthlyPlanDetailDto)
        {
            var planDetail = _monthlyPlanDetailDal.Get(w => w.Id == monthlyPlanDetailDto.Id && w.MonthlyPlanId == monthlyPlanDetailDto.MonthlyPlanId);

            planDetail.Description = monthlyPlanDetailDto.Description;

            _monthlyPlanDetailDal.Update(planDetail);

            return new SuccessResult(Messages.MonthlyPlanDetailUpdated);
        }

        public IResult DeleteDetail(int id)
        {
            var planDetail = _monthlyPlanDetailDal.Get(w => w.Id == id);

            _monthlyPlanDetailDal.Delete(planDetail);

            return new SuccessResult(Messages.MonthlyPlanDetailDeleted);
        }
    }
}