using FluentValidation;
using System;
using TaskManager.Core.Extensions;
using TaskManager.Entities.Concrete;

namespace TaskManager.Business.ValidationRules.FluentValidation
{
    public class WeeklyPlanDtoValidator : AbstractValidator<WeeklyPlan>
    {
        DateTime Today = DateTime.Today;
        public WeeklyPlanDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.WeekNumber).InclusiveBetween(1, 53);
            RuleFor(m => m.WeekNumber).InclusiveBetween(Today.Week(), new DateTime(Today.Year, 12, 31).Week()).When(w => w.Year == Today.Year);
            RuleFor(m => m.Year).InclusiveBetween(Today.Year, 9999);
            RuleFor(m => m.ImportanceTypeId).InclusiveBetween(1, 4);
        }
    }
}
