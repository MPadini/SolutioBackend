using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators
{
    public class OfferedValidator : IClaimStateValidator
    {
        public override async Task<bool> CanChangeState(ClaimState.eId claimState)
        {
            if (claimState == ClaimState.eId.InMonitoring)
            {
                return true;
            }

            if (claimState == ClaimState.eId.Acepted)
            {
                return true;
            }

            throw new ApplicationException(ErrorMessage);
        }
    }
}
