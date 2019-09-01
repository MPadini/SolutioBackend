using System;
using System.Collections.Generic;
using System.Text;
using Solutio.Core.Entities;
using Solutio.Infrastructure.Repositories.Entities;
using Mapster;
using System.Linq;

namespace Solutio.Infrastructure.Repositories.Mappers
{
    public class ClaimMapper : IClaimMapper
    {
        private readonly IClaimStateMapper claimStateMapper;

        public ClaimMapper(IClaimStateMapper claimStateMapper)
        {
            this.claimStateMapper = claimStateMapper;
        }

        public Claim Map(ClaimDB claimDB)
        {
            var claim = claimDB.Adapt<Claim>();
            if (claim == null) return claim;

            if (claim.State != null)
            {
                claim.State = claimStateMapper.Map(claimDB.State);
            }

            if (claimDB.ClaimInsuredPersons != null && claimDB.ClaimInsuredPersons.Any())
            {
                claim.ClaimInsuredPersons = new List<Person>();
                claimDB.ClaimInsuredPersons.ForEach(x => 
                {
                    claim.ClaimInsuredPersons.Add(x.Person.Adapt<Person>());
                });
            }

            if (claimDB.ClaimThirdInsuredPersons != null && claimDB.ClaimThirdInsuredPersons.Any())
            {
                claim.ClaimThirdInsuredPersons = new List<Person>();
                claimDB.ClaimThirdInsuredPersons.ForEach(x =>
                {
                    claim.ClaimThirdInsuredPersons.Add(x.Person.Adapt<Person>());
                });
            }

            if (claimDB.ClaimInsuredVehicles != null && claimDB.ClaimInsuredVehicles.Any())
            {
                claim.ClaimInsuredVehicles = new List<Vehicle>();
                claimDB.ClaimInsuredVehicles.ForEach(x =>
                {
                    claim.ClaimInsuredVehicles.Add(x.Vehicle.Adapt<Vehicle>());
                });
            }

            if (claimDB.ClaimThirdInsuredVehicles != null && claimDB.ClaimThirdInsuredVehicles.Any())
            {
                claim.ClaimThirdInsuredVehicles = new List<Vehicle>();
                claimDB.ClaimThirdInsuredVehicles.ForEach(x =>
                {
                    claim.ClaimThirdInsuredVehicles.Add(x.Vehicle.Adapt<Vehicle>());
                });
            }

            return claim;
        }

        public ClaimDB Map(Claim claim)
        {
            var claimDb = claim.Adapt<ClaimDB>();
            if (claimDb == null) return claimDb;

            if (claim.Adress != null)
            {
                claimDb.Adress = new AdressDB();
                claimDb.Adress = claim.Adress.Adapt<AdressDB>();
            }

            if (claim.ClaimInsuredPersons != null && claim.ClaimInsuredPersons.Any())
            {
                claimDb.ClaimInsuredPersons = new List<ClaimInsuredPersonDB>();
                ClaimInsuredPersonDB claimInsured = ClaimInsuredPersonDB.NewInstance();
                claim.ClaimInsuredPersons.ForEach(person =>
                {
                    claimInsured.Person = person.Adapt<PersonDB>();
                    if (person.Id > 0)
                    {
                        claimInsured.PersonId = person.Id;
                    }
                    if (claimDb.Id > 0)
                    {
                        claimInsured.ClaimId = claimDb.Id;
                    }
                    claimDb.ClaimInsuredPersons.Add(claimInsured);
                });
            }

            if (claim.ClaimThirdInsuredPersons != null && claim.ClaimThirdInsuredPersons.Any())
            {
                claimDb.ClaimThirdInsuredPersons = new List<ClaimThirdInsuredPersonDB>();
                ClaimThirdInsuredPersonDB claimThirdInsured = ClaimThirdInsuredPersonDB.NewInstance();
                claim.ClaimThirdInsuredPersons.ForEach(person =>
                {
                    claimThirdInsured.Person = person.Adapt<PersonDB>();
                    if (person.Id > 0)
                    {
                        claimThirdInsured.PersonId = person.Id;
                    }
                    if (claimDb.Id > 0)
                    {
                        claimThirdInsured.ClaimId = claimDb.Id;
                    }
                    claimDb.ClaimThirdInsuredPersons.Add(claimThirdInsured);
                });
            }

            if (claim.ClaimInsuredVehicles != null && claim.ClaimInsuredVehicles.Any())
            {
                claimDb.ClaimInsuredVehicles = new List<ClaimInsuredVehicleDB>();
                ClaimInsuredVehicleDB claimVehicle = ClaimInsuredVehicleDB.NewInstance();
                claim.ClaimInsuredVehicles.ForEach(vehicle =>
                {
                    claimVehicle.Vehicle = vehicle.Adapt<VehicleDB>();
                    if (vehicle.Id > 0)
                    {
                        claimVehicle.VehicleId = vehicle.Id;
                    }
                    if (claimDb.Id > 0)
                    {
                        claimVehicle.ClaimId = claimDb.Id;
                    }
                    claimDb.ClaimInsuredVehicles.Add(claimVehicle);
                });
            }

            if (claim.ClaimThirdInsuredVehicles != null && claim.ClaimThirdInsuredVehicles.Any())
            {
                claimDb.ClaimThirdInsuredVehicles = new List<ClaimThirdInsuredVehicleDB>();
                ClaimThirdInsuredVehicleDB claimThirdVehicle = ClaimThirdInsuredVehicleDB.NewInstance();
                claim.ClaimThirdInsuredVehicles.ForEach(vehicle =>
                {
                    claimThirdVehicle.Vehicle = vehicle.Adapt<VehicleDB>();
                    if (vehicle.Id > 0)
                    {
                        claimThirdVehicle.VehicleId = vehicle.Id;
                    }
                    if (claimDb.Id > 0)
                    {
                        claimThirdVehicle.ClaimId = claimDb.Id;
                    }
                    claimDb.ClaimThirdInsuredVehicles.Add(claimThirdVehicle);
                });
            }

            return claimDb;
        }

        public List<Claim> Map(List<ClaimDB> claim)
        {
            return claim.Adapt<List<Claim>>();
        }
    }
}
