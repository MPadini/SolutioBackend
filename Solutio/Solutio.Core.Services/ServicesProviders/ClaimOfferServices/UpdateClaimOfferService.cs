using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimOfferServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimOfferServices {
    public class UpdateClaimOfferService : IUpdateClaimOfferService {
        private readonly IClaimOfferRepoitory claimOfferRepoitory;

        public UpdateClaimOfferService(IClaimOfferRepoitory claimOfferRepoitory) {
            this.claimOfferRepoitory = claimOfferRepoitory;
        }


        public async Task Update(Claim claim, List<ClaimOffer> claimOffers) {
            if (claim == null) return;

            await UpdateOrSaveOffer(claim, claimOffers);
        }

        private async Task UpdateOrSaveOffer(Claim claim, List<ClaimOffer> claimOffers) {
            if (claimOffers != null) {
                claimOffers.ForEach(async offer => {
                    var existinOffers = claim.ClaimOffers.FirstOrDefault(x => x.Id == offer.Id);
                    if (existinOffers != null) {
                        await claimOfferRepoitory.Update(offer, existinOffers);
                    } else {
                        await claimOfferRepoitory.Save(offer, claim.Id);
                    }
                });
            }
        }
    }
}
