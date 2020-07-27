using FluentValidation;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.Business.ValidationRules.FluentValidation
{
    public class MonthyPlanDetailDtoValidator : AbstractValidator<MonthlyPlanDetailDto>
    {
        public MonthyPlanDetailDtoValidator()
        {
            RuleFor(m => m.Description).NotEmpty();
        }
    }
}
