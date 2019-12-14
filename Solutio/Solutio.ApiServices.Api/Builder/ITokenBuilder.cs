using Solutio.ApiServices.Api.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Builder
{
    public interface ITokenBuilder
    {
        ITokenBuilder WithUserName(string userName);

        ITokenBuilder WithUserId(int userId);

        ITokenBuilder WithRole(List<string> roles);

        ITokenBuilder WithOfficeId(long officeId);

        JwtSecurityToken Build();
    }
}
