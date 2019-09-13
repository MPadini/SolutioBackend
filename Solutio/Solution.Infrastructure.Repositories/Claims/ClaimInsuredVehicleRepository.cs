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

        public async Task Delete(Claim claim)
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

        public async Task Update(Vehicle existingVehicle, Vehicle vehicleNewData)
        {
            var updatedVehicle = existingVehicle.Adapt<VehicleDB>();

            updatedVehicle.VehicleModel = vehicleNewData.VehicleModel;
            updatedVehicle.VehicleManufacturer = vehicleNewData.VehicleManufacturer;
            updatedVehicle.VehicleTypeId = vehicleNewData.VehicleTypeId;
            updatedVehicle.InsuranceCompany = vehicleNewData.InsuranceCompany;
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
