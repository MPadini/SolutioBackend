﻿using Solutio.ApiServices.Api.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Builder
{
    public interface ITokenBuilder
    {
        ITokenBuilder WithUserInfo(UserInfoDto userInfo);

        JwtSecurityToken Build();
    }
}
