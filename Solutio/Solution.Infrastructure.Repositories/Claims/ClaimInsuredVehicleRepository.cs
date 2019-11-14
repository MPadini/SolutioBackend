using Mapster;
using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Entities;
using Solutio.Infrastructure.Repositories.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimInsuredVehicleRepository : IClaimInsuredVehicleRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IClaimMapper claimMapper;

        public ClaimInsuredVehicleRepository(
            ApplicationDbContext applicationDbContext,
            IClaimMapper claimMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.claimMapper = claimMapper;
        }

        public async Task DeleteAll(Claim claim)
        {
            var claimDB = claimMapper.Map(claim);
            if (claimDB.ClaimInsuredVehicles != null)
            {
                claimDB.ClaimInsuredVehicles.ForEach(async vehicle =>
                {
                    await DeleteClaimVehicle(vehicle);
                });
            }
        }

        public async Task Delete(Claim claim, List<long> vehicleIds)
        {
            var claimDB = claimMapper.Map(claim);
            if (claimDB.ClaimInsuredVehicles == null) return;
            if (vehicleIds == null || !vehicleIds.Any()) return;

            claimDB.ClaimInsuredVehicles.ForEach(async vehicle =>
            {
                if (vehicleIds.Contains(vehicle.VehicleId))
                {
                    await DeleteClaimVehicle(vehicle);
                }
            });
        }

        private async Task DeleteClaimVehicle(ClaimInsuredVehicleDB claimInsuredVehicle)
        {
            var vehicle = claimInsuredVehicle.Vehicle;
            claimInsuredVehicle.Claim = null;
            claimInsuredVehicle.Vehicle = null;

            applicationDbContext.ClaimInsuredVehicles.Remove(claimInsuredVehicle);
            applicationDbContext.Vehicles.Remove(vehicle);
            applicationDbContext.SaveChanges();
        }

        public async Task Update(Vehicle existingVehicle, Vehicle vehicleNewData)
        {
            var updatedVehicle = existingVehicle.Adapt<VehicleDB>();

            updatedVehicle.VehicleModel = vehicleNewData.VehicleModel;
            updatedVehicle.VehicleManufacturer = vehicleNewData.VehicleManufacturer;
            updatedVehicle.VehicleTypeId = vehicleNewData.VehicleTypeId;
            updatedVehicle.InsuranceCompanyId = vehicleNewData.InsuranceCompanyId;
            updatedVehicle.DamageDetail = vehicleNewData.DamageDetail;
            updatedVehicle.Franchise = vehicleNewData.Franchise;
            updatedVehicle.HaveFullCoverage = vehicleNewData.HaveFullCoverage;
            updatedVehicle.Patent = vehicleNewData.Patent;

            applicationDbContext.Vehicles.Update(updatedVehicle);
            applicationDbContext.SaveChanges();
        }

        public async Task Save(Vehicle vehicle, long claimDbId)
        {
            var claimInsured = ClaimInsuredVehicleDB.NewInstance();
            claimInsured.Vehicle = vehicle.Adapt<VehicleDB>();
            claimInsured.ClaimId = claimDbId;
            claimInsured.Claim = null;
            applicationDbContext.ClaimInsuredVehicles.Add(claimInsured);
            applicationDbContext.SaveChanges();
        }
    }
}
