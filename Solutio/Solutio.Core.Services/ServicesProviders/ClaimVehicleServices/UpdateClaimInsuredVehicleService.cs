using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimVehicleServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimVehicleServices
{
    public class UpdateClaimInsuredVehicleService : IUpdateClaimInsuredVehicleService
    {
        private readonly IClaimInsuredVehicleRepository claimInsuredVehicleRepository;

        public UpdateClaimInsuredVehicleService(IClaimInsuredVehicleRepository claimInsuredVehicleRepository)
        {
            this.claimInsuredVehicleRepository = claimInsuredVehicleRepository;
        }

        public async Task UpdateClaimInsuredVehicles(Claim claim, List<Vehicle> vehicles)
        {
            if (claim == null) return;

            await UpdateOrSaveVehicles(claim, vehicles);
        }

        private async Task UpdateOrSaveVehicles(Claim claim, List<Vehicle> vehicles)
        {
            if (vehicles != null)
            {
                vehicles.ForEach(async vehicle =>
                {
                    var existingVehicle = claim.ClaimInsuredVehicles.FirstOrDefault(x => x.Id == vehicle.Id);
                    if (existingVehicle != null)
                    {
                        await claimInsuredVehicleRepository.Update(existingVehicle, vehicle);
                    }
                    else
                    {
                        await claimInsuredVehicleRepository.Save(vehicle, claim.Id);
                    }
                });
            }
        }
    }
}
