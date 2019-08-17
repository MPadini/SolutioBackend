using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Solutio.ApiServices.Api.Dtos;

namespace Solutio.ApiServices.Api.Validators
{
    public class ClaimFileValidator : AbstractValidator<ClaimFileDto>
    {
        public ClaimFileValidator()
        {
            RuleFor(x => x.Base64).NotEmpty();
            RuleFor(x => x.FileExtension).NotEmpty();
            RuleFor(x => x.FileName).NotEmpty();
            RuleFor(x => x.ClaimId).NotEmpty();
        }
    }
}
