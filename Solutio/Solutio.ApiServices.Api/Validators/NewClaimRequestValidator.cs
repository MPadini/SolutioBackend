using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Solutio.ApiServices.Api.Dtos.Requests;

namespace Solutio.ApiServices.Api.Validators
{
    public class NewClaimRequestValidator : AbstractValidator<NewClaimRequest>
    {
        public NewClaimRequestValidator()
        {
            RuleFor(x => x.Hour).MaximumLength(5);
            RuleFor(x => x.StateId).NotEmpty();
        }
    }
}
