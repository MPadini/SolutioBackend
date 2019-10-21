using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimsServices
{
    public interface INewClaimService
    {
        Task<long> Save(Claim claim, string userName);
    }
}
