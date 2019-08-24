using Solutio.ApiServices.Api.Dtos.Requests;
using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Mappers
{
    public interface INewClaimRequestMapper
    {
        Claim Map(NewClaimRequest newClaimRequest);
    }
}
