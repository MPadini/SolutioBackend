using Mapster;
using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Claims {
    public class ClaimOfferRepoitory : IClaimOfferRepoitory {
        private readonly ApplicationDbContext applicationDbContext;

        public ClaimOfferRepoitory(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task Save(ClaimOffer claimOffer, long claimDbId) {
            var claimOfferDB = claimOffer.Adapt<ClaimOfferDB>();

            claimOfferDB.ClaimId = claimDbId;
            applicationDbContext.ClaimOffers.Add(claimOfferDB);
            applicationDbContext.SaveChanges();
        }

        public async Task Update(ClaimOffer offerNewData, ClaimOffer existingOffer) {
            var offerToUpdate = existingOffer.Adapt<ClaimOfferDB>();

            offerToUpdate.AgreementFileId = offerNewData.AgreementFileId;
            offerToUpdate.ClaimOfferStateId = offerNewData.ClaimOfferStateId;
            offerToUpdate.OfferedAmount = offerNewData.OfferedAmount;
            offerToUpdate.PayInstructions = offerNewData.PayInstructions;
            offerToUpdate.SignedAgreementFileId = offerNewData.SignedAgreementFileId;
            offerToUpdate.WayToPay = offerNewData.WayToPay;

            applicationDbContext.ClaimOffers.Update(offerToUpdate);
            applicationDbContext.SaveChanges();
        }
    }
}
