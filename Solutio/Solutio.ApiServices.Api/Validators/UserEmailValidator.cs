using FluentValidation;
using Solutio.ApiServices.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Validators
{
    public class UserEmailValidator : AbstractValidator<UserEmailDto>
    {
        public UserEmailValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
