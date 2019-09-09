using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Solutio.Core.Services.ApplicationServices.AlarmServices;

namespace Solutio.Core.Services.ServicesProviders.ClaimsServices
{
    public class GetClaimService : IGetClaimService
    {
        private readonly IClaimRepository claimRepository;
        private readonly ISetAlarmActivationService setAlarmActivationService;

        public GetClaimService(IClaimRepository claimRepository, ISetAlarmActivationService setAlarmActivationService)
        {
            this.claimRepository = claimRepository;
            this.setAlarmActivationService = setAlarmActivationService;
        }

        public async Task<Claim> GetById(long id)
        {
            var claim = await claimRepository.GetById(id);
            return await setAlarmActivationService.Set(claim);
        }

        public async Task<List<Claim>> GetAll()
        {
            var claims = await claimRepository.GetAll();
            if (claims == null || !claims.Any()) return default;

            claims.ForEach(async claim =>
            {
                await setAlarmActivationService.Set(claim);
            });

            return claims;
        }
    }
}
