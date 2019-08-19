using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsServices
{
    public class NewClaimService : INewClaimService
    {
        private readonly IClaimRepository claimRepository;

        public NewClaimService(IClaimRepository claimRepository)
        {
            this.claimRepository = claimRepository;
        }

        public async Task<long> Save(Claim claim)
        {
            await SetPersons(claim);
            await SetVehicles(claim);

            return await claimRepository.Save(claim);
        }

        private async Task SetPersons(Claim claim)
        {
            //TODO
            //Vaidate if person exists
            claim.ClaimInsuredPersons.ForEach(Person =>
            {
                if (Person.PersonId > 0)
                {
                    Person.Person = null;
                }
            });
        }

        private async Task SetVehicles(Claim claim)
        {
            //TODO
            //Vaidate if vehicle exists
            claim.ClaimInsuredVehicles.ForEach(vehicle =>
            {
                if (vehicle.VehicleId > 0)
                {
                    vehicle.Vehicle = null;
                }
            });
        }
    }
}
