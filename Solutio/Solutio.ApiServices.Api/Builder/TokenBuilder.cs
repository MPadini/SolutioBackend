using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Solutio.ApiServices.Api.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Builder
{
    public class TokenBuilder : ITokenBuilder
    {
        private UserInfoDto userInfo;
        private readonly IConfiguration configuration;

        public TokenBuilder(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
       

        public JwtSecurityToken Build()
        {
            if (userInfo == null) throw new ApplicationException("UserInfo null");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("uniquevalue", "nicolasbjkmpadini"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKeyValue"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return token;
        }

        public ITokenBuilder WithUserInfo(UserInfoDto userInfo)
        {
            this.userInfo = userInfo;
            return this;
        }
    }
}
