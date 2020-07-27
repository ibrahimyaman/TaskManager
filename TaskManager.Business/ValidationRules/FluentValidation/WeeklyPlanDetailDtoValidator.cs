using FluentValidation;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.Business.ValidationRules.FluentValidation
{
    public class WeeklyPlanDetailDtoValidator : AbstractValidator<WeeklyPlanDetailDto>
    {
        public WeeklyPlanDetailDtoValidator()
        {
            RuleFor(m => m.Description).NotEmpty();
        }
    }
}
