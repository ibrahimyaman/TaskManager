using FluentValidation;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.Business.ValidationRules.FluentValidation
{
    public class DailyPlanDetailDtoValidator : AbstractValidator<DailyPlanDetailDto>
    {
        public DailyPlanDetailDtoValidator()
        {
            RuleFor(m => m.Description).NotEmpty();
        }
    }
}
