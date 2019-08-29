using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimsServices
{
    public interface IUpdateClaimService
    {
        Task Update(Claim claim, long claimId);
    }
}
