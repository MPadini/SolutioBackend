using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimDocumentServices;
using Solutio.Core.Services.ApplicationServices.FileService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimDocumentServices {
    public class GetDocumentsByClaimStateService : IGetDocumentsByClaimStateService {
        private readonly IGetFileService getFileService;
        public GetDocumentsByClaimStateService(IGetFileService getFileService) {
            this.getFileService = getFileService;
        }

        public Task<List<ClaimFile>> GetDocumentsByState(Claim claim) {
            //switch (claim.State.Id) {
            //     case (long)ClaimState.eId.Presented: 
            //             break;

            //     case (long)ClaimState.eId.Presented:
            //         break;
            //     case (long)ClaimState.eId.Presented:
            //         break;
            //     case (long)ClaimState.eId.Presented:
            //         break;
            // }
            return null;
        }
    }
}
