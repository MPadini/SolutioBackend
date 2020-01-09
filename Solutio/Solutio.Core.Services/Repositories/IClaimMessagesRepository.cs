using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimMessagesRepository
    {
        Task<long> Upload(ClaimMessage claimMessage);

        Task Delete(ClaimMessage claimMessage);

        Task<ClaimMessage> GetById(long claimMessageId);

        Task DeleteClaimMessages(Claim claim);
       
        Task<List<ClaimMessage>> GetByClaimId(long claimId);

        Task Update(List<ClaimMessage> claimMessage);
    }
}
