using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Solutio.ApiServices.Api.Dtos;
using Solutio.Core.Entities;

namespace Solutio.ApiServices.Api.Mappers
{
    public class ClaimDtoMapper : IClaimDtoMapper
    {
        public Claim Map(ClaimDto claimDto)
        {
            var claim = claimDto.Adapt<Claim>();

            if (claimDto.ClaimAdress != null)
            {
                claim.Adress = new Adress();
                claim.Adress = claimDto.ClaimAdress.Adapt<Adress>();
                claim.Adress.Province = null;
                claim.Adress.City = null;
            }

            if (claimDto.ClaimInsuredPersons != null && claimDto.ClaimInsuredPersons.Any())
            {
                claim.ClaimInsuredPersons = new List<Person>();
                claim.ClaimInsuredPersons = claimDto.ClaimInsuredPersons.Adapt<List<Person>>();
                claim.ClaimInsuredPersons.ForEach(person => person.PersonType = null);
            }

            if (claimDto.ClaimInsuredVehicles != null && claimDto.ClaimInsuredVehicles.Any())
            {
                claim.ClaimInsuredVehicles = new List<Vehicle>();
                claim.ClaimInsuredVehicles = claimDto.ClaimInsuredVehicles.Adapt<List<Vehicle>>();
                claim.ClaimInsuredVehicles.ForEach(vehicle => vehicle.VehicleType = null);
            }

            if (claimDto.ClaimThirdInsuredPersons != null && claimDto.ClaimThirdInsuredPersons.Any())
            {
                claim.ClaimThirdInsuredPersons = new List<Person>();
                claim.ClaimThirdInsuredPersons = claimDto.ClaimThirdInsuredPersons.Adapt<List<Person>>();
                claim.ClaimThirdInsuredPersons.ForEach(person => person.PersonType = null);
            }

            if (claimDto.ClaimThirdInsuredVehicles != null && claimDto.ClaimThirdInsuredVehicles.Any())
            {
                claim.ClaimThirdInsuredVehicles = new List<Vehicle>();
                claim.ClaimThirdInsuredVehicles = claimDto.ClaimThirdInsuredVehicles.Adapt<List<Vehicle>>();
                claim.ClaimThirdInsuredVehicles.ForEach(vehicle => vehicle.VehicleType = null);
            }

            return claim;
        }

        public ClaimDto Map(Claim claim)
        {
            var claimDto = claim.Adapt<ClaimDto>();
            claimDto.ClaimAdress = new AdressDto();
            claimDto.ClaimAdress = claim.Adress.Adapt<AdressDto>();
            claimDto.State = claim.State.Adapt<ClaimStateDto>();

            return claimDto;
        }
    }
}
