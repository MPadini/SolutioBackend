using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices
{
    public abstract class IClaimStateValidator
    {
        protected string ErrorMessage => "No es posible cambiar el estado realizar el cambio de estado solicitado";

        public abstract Task<bool> CanChangeState(ClaimState.eId claimState);
    }
}
 