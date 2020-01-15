using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.FileService;
using System.Linq;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators {
    public class EnRevision : IClaimStateValidator {
        private readonly IGetFileService getFileService;
        private readonly IDeleteFileService deleteFileService;

        public EnRevision(IGetFileService getFileService, IDeleteFileService deleteFileService) {
            this.getFileService = getFileService;
            this.deleteFileService = deleteFileService;
        }

        public override async Task<long> ChangeNextState(Claim claim, long newStateSetedByuser) {

            if (newStateSetedByuser == (long)ClaimState.eId.Borrador) {
                var files = await getFileService.GetByClaimId(claim.Id);
                if (files != null) {
                    var reclamo = files.FirstOrDefault(x => x.FileTypeId == (long)FileType.eId.reclamo);

                    if (reclamo != null) {
                        await deleteFileService.Delete(reclamo);
                    }
                }
            }

            if (newStateSetedByuser > 0) {
                return newStateSetedByuser;
            }

            return (long)ClaimState.eId.En_Revision;
        }
    }
}
