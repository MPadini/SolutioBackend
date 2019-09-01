using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimStateConfigurationRepository
    {
        Task<ClaimStateConfiguration> GetByParentStateId(long parentStateId);
    }
}
