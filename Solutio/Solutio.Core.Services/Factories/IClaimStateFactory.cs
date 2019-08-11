using Solutio.Core.Services.ServicesProviders.ClaimsStatesServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Factories
{
    public interface IClaimStateFactory
    {
        Task<IClaimStateValidator> GetStateValidator(long claimStateId);
    }

}
