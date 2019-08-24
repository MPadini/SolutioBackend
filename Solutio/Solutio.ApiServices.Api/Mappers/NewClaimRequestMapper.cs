using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Solutio.ApiServices.Api.Dtos;
using Solutio.ApiServices.Api.Dtos.Requests;
using Solutio.Core.Entities;

namespace Solutio.ApiServices.Api.Mappers
{
    public class NewClaimRequestMapper : INewClaimRequestMapper
    {
        public Claim Map(NewClaimRequest newClaimRequest)
        {
            var claim = newClaimRequest.Adapt<Claim>();
            if(newClaimRequest.ClaimAdress != null)
            {
                claim.Adress = new Adress();
                claim.Adress =  newClaimRequest.ClaimAdress.Adapt<Adress>();
            }
            
            return claim;
        }
    }
}
