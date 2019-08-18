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
            return await claimRepository.GetById(id);
        }

        public async Task<List<Claim>> GetAll()
        {
            return await claimRepository.GetAll();
        }
    }
}
