using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.AlarmServices;
using Solutio.Core.Services.ApplicationServices.ClaimMessagesService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.AlarmServices
{
    public class SetAlertMessagesService : ISetAlertMessagesService
    {
        private readonly IGetClaimMessagesService getClaimMessagesService;

        public SetAlertMessagesService(
            IGetClaimMessagesService getClaimMessagesService
            )
        {
            this.getClaimMessagesService = getClaimMessagesService;
        }

        public async Task<Claim> Set(Claim claim, int userRole)
        {
            claim.HasNewMessages = false;
            claim.HasMessages = false;
            var messages = await getClaimMessagesService.GetByClaimId(claim.Id);
            //set not new messages
            if (messages != null)
            {
                if (messages.Count > 0)
                {
                    claim.HasMessages = true;
                }
                var thisOne = messages.Find(message => message.Viewed == false && message.UserRoleId != userRole);
                if (thisOne != null)
                {
                    //set new messages
                    claim.HasNewMessages = true;
                }
            }

            return claim;
        }
    }
}
