using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators {
    public class PendienteDePresentación : IClaimStateValidator {
        public override async Task<long> ChangeNextState(Claim claim, long newStateSetedByuser) {
            if (newStateSetedByuser > 0) {
                return newStateSetedByuser;
            }


            return (long)ClaimState.eId.Pendiente_de_Presentación;
        }
    }
}
