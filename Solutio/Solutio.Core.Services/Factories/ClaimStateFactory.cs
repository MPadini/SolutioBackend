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
            if (claimStateId == (long)ClaimState.eId.Borrador)
            {
                return new InDraftValidator();
            }
            //if (claimStateId == (long)ClaimState.eId.a)
            //{
            //    return new AuditValidator();
            //}
            if (claimStateId == (long)ClaimState.eId.Presentado)
            {
                return new PresentedValidator();
            }
            //if (claimStateId == (long)ClaimState.eId.)
            //{
            //    return new WaitForActionValidator();
            //}
            //if (claimStateId == (long)ClaimState.eId.mo)
            //{
            //    return new InMonitoringValidator();
            //}
            if (claimStateId == (long)ClaimState.eId.Nuevo_Ofrecimiento)
            {
                return new OfferedValidator();
            }
            if (claimStateId == (long)ClaimState.eId.Ofrecimiento_Aceptado)
            {
                return new AceptedValidator();
            }
            //if (claimStateId == (long)ClaimState.eId.Outstanding)
            //{
            //    return new OutstandingValidator();
            //}
            if (claimStateId == (long)ClaimState.eId.Cerrado)
            {
                return new CloseValidator();
            }

            throw new ApplicationException("State manager not configured");
        }
    }
}
