using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimsStatesServices
{
    public interface IClaimGetStateService
    {
        Task<List<ClaimState>> GetAll();
    }
}
