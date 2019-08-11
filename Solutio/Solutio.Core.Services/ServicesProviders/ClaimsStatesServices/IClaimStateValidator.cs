using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices
{
    public interface IClaimStateValidator
    {
        Task<bool> CanChangeState(Claim claim);
    }
}
 