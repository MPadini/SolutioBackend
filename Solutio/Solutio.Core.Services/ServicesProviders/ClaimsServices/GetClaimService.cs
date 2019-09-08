using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsServices
{
    public class GetClaimService : IGetClaimService
    {
        private readonly IClaimRepository claimRepository;

        public GetClaimService(IClaimRepository claimRepository)
        {
            this.claimRepository = claimRepository;
        }

        public async Task<Claim> GetById(long id)
        {
            var claim = await claimRepository.GetById(id);
            return claim;
        }

        public async Task<List<Claim>> GetAll()
        {
            return await claimRepository.GetAll();
        }

        private Claim SetAlarmActivation(Claim claim)
        {
            if (claim == null) return claim;
            if (claim.State == null) return claim;

            claim.StateAlarmActive = false;
            var maximumTimeAllowed = claim.State.MaximumTimeAllowed;
            var actualDate = DateTime.Now;

            var dateDifference = actualDate - claim.StateModifiedDate;
            int dateDifferenceInt = int.Parse(dateDifference.TotalHours.ToString());

            if (dateDifferenceInt > maximumTimeAllowed)
            {
                claim.StateAlarmActive = true;
            }

            return claim;
        }
    }
}
