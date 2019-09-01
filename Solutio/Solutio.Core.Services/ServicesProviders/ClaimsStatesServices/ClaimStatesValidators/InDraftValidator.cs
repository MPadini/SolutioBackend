using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators
{
    public class InDraftValidator : IClaimStateValidator
    {
        public override async Task<bool> CanChangeState(ClaimState.eId claimState)
        {
            if (claimState == ClaimState.eId.Presented)
            {
                return true;
            }

            throw new ApplicationException(ErrorMessage);
        }
    }
}
