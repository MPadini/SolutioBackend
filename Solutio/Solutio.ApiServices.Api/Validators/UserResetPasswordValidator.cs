using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Solutio.ApiServices.Api.Dtos;

namespace Solutio.ApiServices.Api.Validators
{
    public class UserResetPasswordValidator : AbstractValidator<UserResetPasswordDto>
    {
        public UserResetPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
