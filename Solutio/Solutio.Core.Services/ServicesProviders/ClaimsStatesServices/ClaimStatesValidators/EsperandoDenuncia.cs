using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators {
    public class EsperandoDenuncia : IClaimStateValidator {
        public override async Task<long> ChangeNextState(Claim claim, long newStateSetedByuser) {
            if (newStateSetedByuser > 0) {
                return newStateSetedByuser;
            }

            //nro de siniestro
            //if (claim) {
            //    return (long)ClaimState.eId.Pendiente_de_Presentación;
            //}

            return (long)ClaimState.eId.Esperando_Denuncia;
        }
    }
}
