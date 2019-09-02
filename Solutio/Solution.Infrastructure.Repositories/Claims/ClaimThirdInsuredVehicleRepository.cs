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
    public class ClaimThirdInsuredVehicleRepository : IClaimThirdInsuredVehicleRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IClaimMapper claimMapper;

        public ClaimThirdInsuredVehicleRepository(
            ApplicationDbContext applicationDbContext,
            IClaimMapper claimMapper)
        {

            this.applicationDbContext = applicationDbContext;
            this.claimMapper = claimMapper;
        }

        public async Task DeleteClaimThirdInsuredVehicles(Claim claim)
        {
            var claimDB = claimMapper.Map(claim);
            if (claimDB.ClaimThirdInsuredVehicles != null)
            {
                claimDB.ClaimThirdInsuredVehicles.ForEach(vehicle =>
                {
                    vehicle.Claim = null;
                    vehicle.Vehicle = null;
                    applicationDbContext.ClaimThirdInsuredVehicles.Remove(vehicle);
                    applicationDbContext.SaveChanges();
                });
            }
        }

        public async Task<Claim> UpdateClaimThirdInsuredVehicles(Claim claim, List<Vehicle> vehicles)
        {
            var claimDb = claimMapper.Map(claim);
            if (claimDb.ClaimThirdInsuredVehicles == null || vehicles == null) return default;

            vehicles.ForEach(vehicle =>
            {
                var thirdInsuredVehicle = claimDb.ClaimThirdInsuredVehicles.FirstOrDefault(x => x.VehicleId == vehicle.Id);
                if (thirdInsuredVehicle != null)
                {
                    thirdInsuredVehicle.Vehicle.VehicleModel = vehicle.VehicleModel;
                    thirdInsuredVehicle.Vehicle.VehicleManufacturer = vehicle.VehicleManufacturer;
                    thirdInsuredVehicle.Vehicle.VehicleTypeId = vehicle.VehicleTypeId;
                    thirdInsuredVehicle.Vehicle.InsuranceCompany = vehicle.InsuranceCompany;
                    thirdInsuredVehicle.Vehicle.DamageDetail = vehicle.DamageDetail;
                    thirdInsuredVehicle.Vehicle.Franchise = vehicle.Franchise;
                    thirdInsuredVehicle.Vehicle.HaveFullCoverage = vehicle.HaveFullCoverage;
                    thirdInsuredVehicle.Vehicle.Patent = vehicle.Patent;

                    applicationDbContext.Vehicles.Update(thirdInsuredVehicle.Vehicle);
                    applicationDbContext.SaveChanges();
                }
                else
                {
                    var claimThirdInsured = ClaimThirdInsuredVehicleDB.NewInstance();
                    claimThirdInsured.Vehicle = vehicle.Adapt<VehicleDB>();
                    claimThirdInsured.ClaimId = claimDb.Id;
                    claimThirdInsured.Claim = null;
                    applicationDbContext.ClaimThirdInsuredVehicles.Add(claimThirdInsured);
                    applicationDbContext.SaveChanges();
                }
            });

            return claimMapper.Map(claimDb);
        }
    }
}
