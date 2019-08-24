using Mapster;
using Solutio.ApiServices.Api.Dtos;
using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Mappers
{
    public class GetClaimByIdMapper : IGetClaimByIdMapper
    {
        public ClaimDto Map(Claim claim)
        {
            var claimDto = claim.Adapt<ClaimDto>();
            claimDto.ClaimAdress = new AdressDto();
            claimDto.ClaimAdress = claim.Adress.Adapt<AdressDto>();

            return claimDto;
        }
    }
}
