using FluentValidation;
using TaskManager.Core.Constants;
using TaskManager.Core.Utilities.Security.Jwt;

namespace Takas.Business.ValidationRules.FluentValidation
{
    public class RefreshTokenValidator : AbstractValidator<AccessToken>
    {
        public RefreshTokenValidator()
        {
            RuleFor(rt => rt.Token).NotEmpty();
            RuleFor(rt => rt.RefreshToken).NotEmpty().Length(Numbers.RefreshTokenLength);
        }
    }
}
