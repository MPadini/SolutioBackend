using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimOfferServices {
    public interface IUpdateClaimOfferService {

        Task Update(Claim claim, List<ClaimOffer> claimOffers);
    }
}
