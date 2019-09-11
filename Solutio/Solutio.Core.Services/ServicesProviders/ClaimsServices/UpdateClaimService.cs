using Mapster;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.AdressServices;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.Repositories;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Solutio.Core.Services.ServicesProviders.ClaimsServices
{
    public class UpdateClaimService : IUpdateClaimService
    {
        private readonly IClaimRepository claimRepository;
        private readonly IUpdateAdressService updateAdressService;

        public UpdateClaimService(
            IClaimRepository claimRepository,
            IUpdateAdressService updateAdressService)
        {
            this.claimRepository = claimRepository;
            this.updateAdressService = updateAdressService;
        }

        public async Task Update(Claim claim, long claimId)
        {
            var claimDb = await claimRepository.GetById(claimId);
            if (claimDb == null) return;

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var updatedClaim = await UpdateData(claimDb, claim);

                await claimRepository.Update(updatedClaim, claimId);
                await UpdateAssociatedEntities(claimDb, claim);

                scope.Complete();
            }  
        }

        private async Task<Claim> UpdateData(Claim claimDb, Claim claimNewData)
        {
            claimDb.Story = claimNewData.Story;
            claimDb.Hour = claimNewData.Hour;
            claimDb.TotalBudgetAmount = claimNewData.TotalBudgetAmount;
            claimDb.Date = claimNewData.Date;

            if (claimNewData.Adress != null)
            {
                var adress = await updateAdressService.UpdateClaimAdress(claimDb, claimNewData.Adress);
                claimDb.Adress = adress;
            }
           
            return claimDb;
        }

        private async Task UpdateAssociatedEntities(Claim existingClaim, Claim claimNewData)
        {
            //var existingClaim = claimMapper.Map(claimDB);
            //await claimInsuredPersonRepository.UpdateClaimInsuredPersons(existingClaim, claim.ClaimInsuredPersons);
            //await claimInsuredVehicleRepository.UpdateClaimInsuredVehicles(existingClaim, claim.ClaimInsuredVehicles);
            //await claimThirdInsuredPersonRepository.UpdateClaimThirdInsuredPersons(existingClaim, claim.ClaimThirdInsuredPersons);
            //await claimThirdInsuredVehicleRepository.UpdateClaimThirdInsuredVehicles(existingClaim, claim.ClaimThirdInsuredVehicles);
        }
    }
}
