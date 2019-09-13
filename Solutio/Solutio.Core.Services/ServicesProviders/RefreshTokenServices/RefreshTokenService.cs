using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.RefreshTokenServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.RefreshTokenServices
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> Get(string userName)
        {
           return await refreshTokenRepository.Get(userName);
        }

        public async Task<RefreshToken> GetByRefreshToken(string refreshToken)
        {
            return await refreshTokenRepository.GetByRefreshToken(refreshToken);
        }

        public async Task Revoke(RefreshToken refreshToken)
        {
            await refreshTokenRepository.Revoke(refreshToken);
        }

        public async Task Save(RefreshToken refreshToken)
        {
            await refreshTokenRepository.Save(refreshToken);
        }
    }
}
