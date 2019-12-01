using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimDocumentServices {
    public interface IGetDocumentsByClaimStateService {

        Task<List<ClaimFile>> GetDocumentsByState(Claim claim);
    }
}
