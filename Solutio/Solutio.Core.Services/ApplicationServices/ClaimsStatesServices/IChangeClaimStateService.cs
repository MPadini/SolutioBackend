using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimsStatesServices
{
    public interface IChangeClaimStateService
    {
        Task<bool> ChangeState(Claim claim, long newStateId);

        Task SendClaimsToAjuicio();
    }
}
