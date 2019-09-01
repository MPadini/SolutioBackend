using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators
{
    public class CloseValidator : IClaimStateValidator
    {
        public override async Task<bool> CanChangeState(ClaimState.eId claimState)
        {
            throw new ApplicationException("El reclamo fue cerrado, no es posible modificar su estado.");
        }
    }
}
