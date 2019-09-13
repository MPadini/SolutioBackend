using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task Save(RefreshToken refreshToken);

        Task Revoke(RefreshToken refreshToken);

        Task<RefreshToken> Get(string userName);

        Task<RefreshToken> GetByRefreshToken(string refreshToken);
    }
}
