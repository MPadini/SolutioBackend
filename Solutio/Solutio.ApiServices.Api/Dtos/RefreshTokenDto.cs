﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class RefreshTokenDto
    {
        public string Email { get; set; }

        public string RefreshToken { get; set; }
    }
}
