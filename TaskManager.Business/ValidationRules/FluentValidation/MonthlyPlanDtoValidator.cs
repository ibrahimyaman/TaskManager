using FluentValidation;
using System;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.Business.ValidationRules.FluentValidation
{
    public class MonthlyPlanDtoValidator : AbstractValidator<MonthlyPlanDto>
    {
        DateTime Today = DateTime.Today;
        public MonthlyPlanDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Month).InclusiveBetween(1, 12);
            RuleFor(m => m.Month).GreaterThanOrEqualTo(Today.Month).When(w => w.Year == Today.Year);
            RuleFor(m => m.Year).InclusiveBetween(Today.Year, 9999);
            RuleFor(m => m.ImportanceTypeId).InclusiveBetween(1,4);
        }
    }
}
