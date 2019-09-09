using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.AlarmServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.AlarmServices
{
    public class SetAlarmActivationService : ISetAlarmActivationService
    {
        public async Task<Claim> Set(Claim claim)
        {
            if (claim == null) return claim;
            if (claim.State == null) return claim;

            claim.StateAlarmActive = false;
            var maximumTimeAllowed = claim.State.MaximumTimeAllowed;
            var actualDate = DateTime.Now;

            var dateDifference = actualDate - claim.StateModifiedDate;

            if (dateDifference.TotalHours > (double)maximumTimeAllowed)
            {
                claim.StateAlarmActive = true;
            }

            return claim;
        }
    }
}
