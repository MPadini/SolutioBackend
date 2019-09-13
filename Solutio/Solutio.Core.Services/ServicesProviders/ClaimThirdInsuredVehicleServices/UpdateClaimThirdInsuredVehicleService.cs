using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimThirdInsuredVehicleServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimThirdInsuredVehicleServices
{
    public class UpdateClaimThirdInsuredVehicleService : IUpdateClaimThirdInsuredVehicleService
    {
        private readonly IClaimThirdInsuredVehicleRepository claimThirdInsuredVehicleRepository;

        public UpdateClaimThirdInsuredVehicleService(IClaimThirdInsuredVehicleRepository claimThirdInsuredVehicleRepository)
        {
            this.claimThirdInsuredVehicleRepository = claimThirdInsuredVehicleRepository;
        }

        public async Task UpdateClaimInsuredVehicles(Claim claim, List<Vehicle> vehicles)
        {
            if (claim != null && vehicles != null)
            {
                vehicles.ForEach(async vehicle =>
                {
                    var existingVehicle = claim.ClaimThirdInsuredVehicles.FirstOrDefault(x => x.Id == vehicle.Id);
                    if (existingVehicle != null)
                    {
                        await claimThirdInsuredVehicleRepository.Update(existingVehicle, vehicle);
                    }
                    else
                    {
                        await claimThirdInsuredVehicleRepository.Save(vehicle, claim.Id);
                    }
                });
            }
        }
    }
}
