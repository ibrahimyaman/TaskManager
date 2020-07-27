using FluentValidation;
using System;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.Business.ValidationRules.FluentValidation
{
    public class DailyPlanDtoValidator : AbstractValidator<DailyPlanDto>
    {
        public DailyPlanDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Date).GreaterThanOrEqualTo(DateTime.Today);           
            RuleFor(m => m.ImportanceTypeId).InclusiveBetween(1, 4);
        }
    }
}
