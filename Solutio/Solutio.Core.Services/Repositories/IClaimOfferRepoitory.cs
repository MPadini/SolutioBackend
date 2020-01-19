using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories {
    public interface IClaimOfferRepoitory {

        Task Save(ClaimOffer claimOffer, long claimDbId);

        Task Update(ClaimOffer offerNewData, ClaimOffer existingOffer);
    }
}
