using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimDocumentServices;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators {
    public class Borrador : IClaimStateValidator {

        public override async Task<long> ChangeNextState(Claim claim, long newStateSetedByuser) {
            if (newStateSetedByuser > 0) {
                return newStateSetedByuser;
            }

            var state = (long)ClaimState.eId.Borrador;

            if (claim == null) return state;

            if (claim.Files == null) {
                return state;
            }

            if (!claim.Files.Any()) {
                return state;
            }

            var reclamo = claim.Files.FirstOrDefault(x => x.FileTypeId == (long)FileType.eId.reclamo);
            if (reclamo == null) {
                return state;
            }

            return (long)ClaimState.eId.En_Revision;
        }
    }
}
