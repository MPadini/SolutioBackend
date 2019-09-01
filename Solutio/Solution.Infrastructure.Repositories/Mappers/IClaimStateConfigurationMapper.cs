using Solutio.Core.Entities;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Mappers
{
    public interface IClaimStateConfigurationMapper
    {
        ClaimStateConfiguration Map(List<ClaimStateConfigurationDB> claimStateConfigurations);
    }
}
