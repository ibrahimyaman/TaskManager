using FluentValidation;
using System;
using System.Linq;
using TaskManager.Core.Exceptions;

namespace TaskManager.Core.CrossCuttingConcerns.Validation
{
    public class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                string validationMessage =
                    string.Join(Environment.NewLine, result.Errors.Select(s => s.ErrorMessage).ToArray());
                throw new CustomException(validationMessage);
            }

        }
    }
}
