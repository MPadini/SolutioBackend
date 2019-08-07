using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class UserConfirmEmailDto
    {
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}
