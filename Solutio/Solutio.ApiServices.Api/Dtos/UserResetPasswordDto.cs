using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class UserResetPasswordDto
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string NewPassword { get; set; }
    }
}
