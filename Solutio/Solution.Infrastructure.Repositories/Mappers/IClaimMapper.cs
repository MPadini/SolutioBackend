using Solutio.Core.Entities;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Mappers
{
    public interface IClaimMapper
    {
        Claim Map(ClaimDB claimDB);

        ClaimDB Map(Claim claimDB);

        List<Claim> Map(List<ClaimDB> claimsDB);
    }
}
