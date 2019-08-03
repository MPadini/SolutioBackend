using FluentValidation;
using Solutio.ApiServices.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Validators
{
    public class UserInfoValidator : AbstractValidator<UserInfoDto>
    {
        public UserInfoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
