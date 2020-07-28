using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder <T, string>  Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[a-zA-Z]").WithMessage("Password must contain a letter")
                .Matches("[0-9]").WithMessage("Password must contain a digit");
            return options;
        }
    }
}
