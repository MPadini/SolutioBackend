using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators {
    public class OfrecimientoAceptado : IClaimStateValidator {
        public override async Task<long> ChangeNextState(Claim claim, long newStateSetedByuser) {
            return (long)ClaimState.eId.Ofrecimiento_Aceptado;
        }
    }
}
