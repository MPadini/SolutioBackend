using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;
using Solutio.Core.Services.ServicesProviders.ClaimsStatesServices;
using Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators;

namespace Solutio.Core.Services.Factories
{
    public class ClaimStateFactory : IClaimStateFactory
    {
        public async Task<IClaimStateValidator> GetStateValidator(long claimStateId)
        {
            if (claimStateId == (long)ClaimState.eId.InDraft)
            {
                return new InDraftValidator();
            }
            if (claimStateId == (long)ClaimState.eId.Audit)
            {
                return new AuditValidator();
            }
            if (claimStateId == (long)ClaimState.eId.Presented)
            {
                return new PresentedValidator();
            }
            if (claimStateId == (long)ClaimState.eId.WaitForAction)
            {
                return new WaitForActionValidator();
            }
            if (claimStateId == (long)ClaimState.eId.InMonitoring)
            {
                return new InMonitoringValidator();
            }
            if (claimStateId == (long)ClaimState.eId.Offered)
            {
                return new OfferedValidator();
            }
            if (claimStateId == (long)ClaimState.eId.Acepted)
            {
                return new AceptedValidator();
            }
            if (claimStateId == (long)ClaimState.eId.Outstanding)
            {
                return new OutstandingValidator();
            }
            if (claimStateId == (long)ClaimState.eId.Close)
            {
                return new CloseValidator();
            }

            throw new ApplicationException("State manager not configured");
        }
    }
}
