using FluentValidation;
using TaskManager.Entities.Concrete.Dtos;

namespace Takas.Business.ValidationRules.FluentValidation
{
    public class UserLoginDtoValidator: AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(k => k.Email).EmailAddress();
            RuleFor(k => k.Password).NotEmpty();
        }
    }
}
