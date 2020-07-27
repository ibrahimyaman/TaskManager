using FluentValidation;
using TaskManager.Core.Extensions;
using TaskManager.Entities.Concrete.Dtos;

namespace Takas.Business.ValidationRules.FluentValidation
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
        {
            RuleFor(k => k.Email).EmailAddress();
            RuleFor(k => k.Password).Password(8).When(w=>!w.Password.IsNullOrWhiteSpace()).WithMessage("Parola güvenli değil");
            RuleFor(k => k.Name).NotEmpty();
            RuleFor(k => k.Surname).NotEmpty();
        }
    }
}
