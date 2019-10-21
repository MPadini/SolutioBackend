using FluentValidation;
using Solutio.ApiServices.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Validators
{
    public class NewUserValidator : AbstractValidator<NewUserDto>
    {
        public NewUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.RoleName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
