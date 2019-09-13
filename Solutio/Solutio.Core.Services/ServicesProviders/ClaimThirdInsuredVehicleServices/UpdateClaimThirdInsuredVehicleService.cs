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
            if (claim == null) return;

            await UpdateOrSaveVehicles(claim, vehicles);
            await DeleteVehicles(claim, vehicles);
        }

        private async Task UpdateOrSaveVehicles(Claim claim, List<Vehicle> vehicles)
        {
            if (vehicles != null)
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

        private async Task DeleteVehicles(Claim claim, List<Vehicle> vehicles)
        {
            if (claim.ClaimThirdInsuredVehicles == null) return;

            if (vehicles == null || !vehicles.Any())
            {
                await claimThirdInsuredVehicleRepository.DeleteAll(claim);
            }
            else
            {
                var vehiclesToDelete = claim.ClaimThirdInsuredVehicles.Select(x => x.Id)
                    .Except(vehicles.Select(x => x.Id))
                    .ToList();

                if (vehiclesToDelete != null && vehiclesToDelete.Any())
                {
                    await claimThirdInsuredVehicleRepository.Delete(claim, vehiclesToDelete);
                }
            }
        }
    }
}
