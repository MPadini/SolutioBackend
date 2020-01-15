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

        public async Task<List<Claim>> GetAll(string userName, long officeId)
        {
            var claims = await claimRepository.GetAll();
            if (claims == null || !claims.Any()) return default;

            var userToSearch = string.IsNullOrWhiteSpace(userName) ? "" : userName;

            claims = claims.Where(x => userToSearch == "" || x.UserName.ToLower().Equals(userToSearch.ToLower())).ToList();
            if (claims == null || !claims.Any()) return default;

            if (officeId > 0) {
                claims = claims.Where(x => x.OfficeId == officeId).ToList();
                if (claims == null || !claims.Any()) return default;
            }
           

            claims.ForEach(async claim =>
            {
                await setAlarmActivationService.Set(claim);
            });

            return claims;
        }

        public async Task<List<Claim>> GetClaimByInsuranceCompany(long insuranceCompany) {
            return await claimRepository.GetClaimByInsuranceCompany(insuranceCompany);
        }

        public async Task<List<Claim>> GetClaimByState(long stateId) {
            return await claimRepository.GetClaimByState(stateId);
        }



        //public async Task<List<Claim>> GetAllGroupByCompany()
        //{
        //    var claims = await claimRepository.GetAll();
        //    if (claims == null || !claims.Any()) return default;

        //    claims.ForEach(async claim =>
        //    {
        //        await setAlarmActivationService.Set(claim);
        //    });

        //    return claims;
        //}
    }
}
