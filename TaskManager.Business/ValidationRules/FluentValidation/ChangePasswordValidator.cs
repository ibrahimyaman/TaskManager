using FluentValidation;
using TaskManager.Core.Extensions;
using TaskManager.Entities.Concrete.Dtos;

namespace Takas.Business.ValidationRules.FluentValidation
{
    public class UserChangePassordDtoValidator : AbstractValidator<UserChangePassordDto>
    {
        public UserChangePassordDtoValidator()
        {
            RuleFor(m => m.NewPassword).Password(8);
        }
    }
}
