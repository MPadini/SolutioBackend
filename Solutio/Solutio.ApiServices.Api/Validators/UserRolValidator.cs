using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Solutio.ApiServices.Api.Dtos;

namespace Solutio.ApiServices.Api.Validators
{
    public class UserRolValidator : AbstractValidator<UserRolDto>
    {
        public UserRolValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.RoleName).NotEmpty();
        }
    }
}
