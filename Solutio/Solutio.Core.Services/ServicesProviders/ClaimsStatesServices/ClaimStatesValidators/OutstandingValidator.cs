using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators
{
    public class OutstandingValidator : IClaimStateValidator
    {
        public override async Task<bool> CanChangeState(ClaimState.eId claimState)
        {
            if (claimState == ClaimState.eId.Offered)
            {
                return true;
            }

            if (claimState == ClaimState.eId.Acepted)
            {
                return true;
            }

            if (claimState == ClaimState.eId.Close)
            {
                return true;
            }

            throw new ApplicationException(ErrorMessage);
        }
    }
}
