using Mapster;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.AdressServices;
using Solutio.Core.Services.ApplicationServices.ClaimPersonServices;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.ApplicationServices.ClaimThirdInsuredPerson;
using Solutio.Core.Services.ApplicationServices.ClaimThirdInsuredVehicleServices;
using Solutio.Core.Services.ApplicationServices.ClaimVehicleServices;
using Solutio.Core.Services.Repositories;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Solutio.Core.Services.ServicesProviders.ClaimsServices
{
    public class UpdateClaimService : IUpdateClaimService
    {
        private readonly IClaimRepository claimRepository;
        private readonly IUpdateAdressService updateAdressService;
        private readonly IUpdateClaimInsuredPersonService updateClaimInsuredPersonService;
        private readonly IUpdateClaimThirdInsuredPersonService updateClaimThirdInsuredPersonService;
        private readonly IUpdateClaimInsuredVehicleService updateClaimInsuredVehicleService;
        private readonly IUpdateClaimThirdInsuredVehicleService updateClaimThirdInsuredVehicleService;

        public UpdateClaimService(
            IClaimRepository claimRepository,
            IUpdateAdressService updateAdressService,
            IUpdateClaimInsuredPersonService updateClaimInsuredPersonService,
            IUpdateClaimThirdInsuredPersonService updateClaimThirdInsuredPersonService,
            IUpdateClaimInsuredVehicleService updateClaimInsuredVehicleService,
            IUpdateClaimThirdInsuredVehicleService updateClaimThirdInsuredVehicleService)
        {
            this.claimRepository = claimRepository;
            this.updateAdressService = updateAdressService;
            this.updateClaimInsuredPersonService = updateClaimInsuredPersonService;
            this.updateClaimThirdInsuredPersonService = updateClaimThirdInsuredPersonService;
            this.updateClaimInsuredVehicleService = updateClaimInsuredVehicleService;
            this.updateClaimThirdInsuredVehicleService = updateClaimThirdInsuredVehicleService;
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
            await updateClaimInsuredPersonService.UpdateClaimInsuredPersons(existingClaim, claimNewData.ClaimInsuredPersons);
            await updateClaimThirdInsuredPersonService.UpdateClaimThirdInsuredPersons(existingClaim, claimNewData.ClaimThirdInsuredPersons);
            await updateClaimInsuredVehicleService.UpdateClaimInsuredVehicles(existingClaim, claimNewData.ClaimInsuredVehicles);
            await updateClaimThirdInsuredVehicleService.UpdateClaimInsuredVehicles(existingClaim, claimNewData.ClaimThirdInsuredVehicles);
        }

        public async Task MarkAsPrinted(List<Claim> claims) {
            if (claims == null) return;
            if (!claims.Any()) return;

            await claimRepository.MarkAsPrinted(claims);
        }
    }
}
