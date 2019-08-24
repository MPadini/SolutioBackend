using Solutio.ApiServices.Api.Dtos;
using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Mappers
{
    public interface IGetClaimByIdMapper
    {
        ClaimDto Map(Claim claim);
    }
}
