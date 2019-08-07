using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Solutio.ApiServices.Api.Dtos;

namespace Solutio.ApiServices.Api.Validators
{
    public class UserConfirmEmailValidator : AbstractValidator<UserConfirmEmailDto>
    {
        public UserConfirmEmailValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
