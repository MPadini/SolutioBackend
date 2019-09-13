using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Solutio.Infrastructure.Repositories.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RefreshTokenRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task Revoke(RefreshToken refreshToken)
        {
            var tokenToRevoke = refreshToken.Adapt<RefreshTokenDB>();
            applicationDbContext.RefreshTokens.Remove(tokenToRevoke);
            applicationDbContext.SaveChanges();
        }

        public async Task Save(RefreshToken refreshToken)
        {
            var tokenToRevoke = refreshToken.Adapt<RefreshTokenDB>();
            applicationDbContext.RefreshTokens.Add(tokenToRevoke);
            applicationDbContext.SaveChanges();
        }

        public async Task<RefreshToken> Get(string userName)
        {
            var refreshToke =  applicationDbContext.RefreshTokens.AsNoTracking().FirstOrDefault(x => x.UserName.Equals(userName));
            return refreshToke.Adapt<RefreshToken>();
        }

        public async Task<RefreshToken> GetByRefreshToken(string refreshToken)
        {
            var refreshTokenDb = applicationDbContext.RefreshTokens.AsNoTracking().FirstOrDefault(x => x.Refreshtoken.Equals(refreshToken));
            return refreshTokenDb.Adapt<RefreshToken>();
        }
    }
}
