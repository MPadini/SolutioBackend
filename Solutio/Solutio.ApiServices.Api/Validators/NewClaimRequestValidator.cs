using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Solutio.ApiServices.Api.Dtos;

namespace Solutio.ApiServices.Api.Validators
{
    public class NewClaimRequestValidator : AbstractValidator<ClaimDto>
    {
        public NewClaimRequestValidator()
        {
            RuleFor(x => x.Hour).MaximumLength(5);
            RuleFor(x => x.StateId).NotEmpty();
            RuleFor(x => x.Files).Null();
        }
    }
}
