using FluentValidation;
using TaskManager.Core.Extensions;
using TaskManager.Entities.Concrete.Dtos;

namespace Takas.Business.ValidationRules.FluentValidation
{
    public class UserRegisterValidator: AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(k => k.Email).EmailAddress();
            RuleFor(k => k.Password).Password(8).WithMessage("Parola güvenli değil");
            RuleFor(k => k.Name).NotEmpty();
            RuleFor(k => k.Surname).NotEmpty();
        }
    }
}
