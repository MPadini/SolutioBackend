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

        public async Task DeleteClaimInsuredVehicles(Claim claim)
        {
            var claimDB = claimMapper.Map(claim);
            if (claimDB.ClaimInsuredVehicles != null)
            {
                claimDB.ClaimInsuredVehicles.ForEach(vehicle =>
                {
                    vehicle.Claim = null;
                    vehicle.Vehicle = null;
                    applicationDbContext.ClaimInsuredVehicles.Remove(vehicle);
                    applicationDbContext.SaveChanges();
                });
            }
        }

        private async Task Update(Vehicle vehicle, ClaimInsuredVehicleDB insuredVehicle)
        {
            insuredVehicle.Vehicle.VehicleModel = vehicle.VehicleModel;
            insuredVehicle.Vehicle.VehicleManufacturer = vehicle.VehicleManufacturer;
            insuredVehicle.Vehicle.VehicleTypeId = vehicle.VehicleTypeId;
            insuredVehicle.Vehicle.InsuranceCompany = vehicle.InsuranceCompany;
            insuredVehicle.Vehicle.DamageDetail = vehicle.DamageDetail;
            insuredVehicle.Vehicle.Franchise = vehicle.Franchise;
            insuredVehicle.Vehicle.HaveFullCoverage = vehicle.HaveFullCoverage;
            insuredVehicle.Vehicle.Patent = vehicle.Patent;

            applicationDbContext.Vehicles.Update(insuredVehicle.Vehicle);
            applicationDbContext.SaveChanges();
        }

        private async Task Save(Vehicle vehicle, long claimDbId)
        {
            var claimInsured = ClaimInsuredVehicleDB.NewInstance();
            claimInsured.Vehicle = vehicle.Adapt<VehicleDB>();
            claimInsured.ClaimId = claimDbId;
            claimInsured.Claim = null;
            applicationDbContext.ClaimInsuredVehicles.Add(claimInsured);
            applicationDbContext.SaveChanges();
        }

        public async Task<Claim> UpdateClaimInsuredVehicles(Claim claim, List<Vehicle> vehicles)
        {
            var claimDb = claimMapper.Map(claim);
            if (claimDb.ClaimInsuredVehicles == null || vehicles == null) return default;

            vehicles.ForEach(async vehicle =>
            {
                var insuredVehicle = claimDb.ClaimInsuredVehicles.FirstOrDefault(x => x.VehicleId == vehicle.Id);
                if (insuredVehicle != null)
                {
                    await Update(vehicle, insuredVehicle);
                }
                else
                {
                    await Save(vehicle, claimDb.Id);
                }
            });

            return claimMapper.Map(claimDb);
        }
    }
}
