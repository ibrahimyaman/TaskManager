using FluentValidation;

namespace TaskManager.Core.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 14)
        {
            var options = ruleBuilder
                .NotEmpty()
                .MinimumLength(minimumLength)
                .Matches("[A-Z]")
                .Matches("[a-z]")
                .Matches("[0-9]")
                .Matches("[^a-zA-Z0-9]");
            return options;
        }
    }
}
