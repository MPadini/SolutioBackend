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

            if (claimDB.ClaimMessages != null && claimDB.ClaimMessages.Any())
            {
                claim.ClaimMessages = new List<ClaimMessage>();
                claimDB.ClaimMessages.ForEach(x =>
                {
                    claim.ClaimMessages.Add(x.Adapt<ClaimMessage>());
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

            if (claim.State != null)
            {
                claimDb.State = null;
                claimDb.StateId = claim.StateId;
            }

            if (claim.ClaimInsuredPersons != null && claim.ClaimInsuredPersons.Any())
            {
                claimDb.ClaimInsuredPersons = new List<ClaimInsuredPersonDB>();
                claim.ClaimInsuredPersons.ForEach(person =>
                {
                    ClaimInsuredPersonDB claimInsured = ClaimInsuredPersonDB.NewInstance();
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
                claim.ClaimThirdInsuredPersons.ForEach(person =>
                {
                    ClaimThirdInsuredPersonDB claimThirdInsured = ClaimThirdInsuredPersonDB.NewInstance();
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
                claim.ClaimInsuredVehicles.ForEach(vehicle =>
                {
                    ClaimInsuredVehicleDB claimVehicle = ClaimInsuredVehicleDB.NewInstance();
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
                claim.ClaimThirdInsuredVehicles.ForEach(vehicle =>
                {
                    ClaimThirdInsuredVehicleDB claimThirdVehicle = ClaimThirdInsuredVehicleDB.NewInstance();
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

            if (claim.ClaimMessages != null && claim.ClaimMessages.Any())
            {
                claimDb.ClaimMessages = new List<ClaimMessageDB>();
                claim.ClaimThirdInsuredVehicles.ForEach(claimMessage =>
                {
                    claimDb.ClaimMessages.Add(claimMessage.Adapt<ClaimMessageDB>());
                });
            }

            return claimDb;
        }

        public List<Claim> Map(List<ClaimDB> claim)
        {
            List<Claim> claims = new List<Claim>();
            foreach (var c in claim)
            {
                var result = Map(c);
                claims.Add(result);
            }

            return claims;
        }
    }
}
