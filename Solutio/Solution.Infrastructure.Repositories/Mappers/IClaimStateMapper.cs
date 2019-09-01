using Solutio.Core.Entities;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Mappers
{
    public interface IClaimStateMapper
    {
        List<ClaimState> Map(List<ClaimStateDB> claimStateDBs);

        ClaimState Map(ClaimStateDB claimStateDB);
    }
}
