using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Solutio.Infrastructure.Repositories.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Solutio.Infrastructure.Repositories.Mappers;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimMessagesRepository : IClaimMessagesRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IClaimMapper claimMapper;

        public ClaimMessagesRepository(ApplicationDbContext applicationDbContext, IClaimMapper claimMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.claimMapper = claimMapper;
        }
      
        public async Task<long> Upload(ClaimMessage claimMessage)
        {
            try
            {
                var claimMessageDb = claimMessage.Adapt<ClaimMessageDB>();

                applicationDbContext.Add(claimMessageDb);
                applicationDbContext.SaveChanges();

                return claimMessageDb.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Delete(ClaimMessage claimMessage)
        {
            var claimMessageDb = claimMessage.Adapt<ClaimMessageDB>();

            applicationDbContext.Remove(claimMessageDb);
            applicationDbContext.SaveChanges();
        }

        public async Task<ClaimMessage> GetById(long claimMessageId)
        {
            var claimMessage = applicationDbContext.ClaimMessages
               .Where(x => x.Id == claimMessageId).Select(x => new {
                   x.Id,
                   x.ClaimId,
                   x.Message,
                   x.UserId,
                   x.Viewed,
                   x.Created,
                   x.Modified
               }).FirstOrDefault();

            return claimMessage.Adapt<ClaimMessage>();
        }

        public async Task<List<ClaimMessage>> GetByClaimId(long claimId)
        {
            var messages = applicationDbContext.ClaimMessages.AsNoTracking()
                .Where(x => x.ClaimId == claimId).Select(x => new {
                    x.Id,
                    x.ClaimId,
                    x.Message,
                    x.UserId,
                    x.Viewed,
                    x.Created,
                    x.Modified
                }).ToList();

            return messages.Adapt<List<ClaimMessage>>();
        }

        public async Task Update(List<ClaimMessage> claimMessages)
        {
            if (claimMessages == null) return;

            var claimMessagesDB = claimMessages.Adapt<List<ClaimMessageDB>>();
            List<ClaimMessageDB> updatedList = new List<ClaimMessageDB>();

            foreach (var claimMessage in claimMessagesDB)
            {
                claimMessage.Viewed = true;
                updatedList.Add(claimMessage);
            }

            if (updatedList.Any())
            {
                applicationDbContext.UpdateRange(updatedList);
                applicationDbContext.SaveChanges();
            }
        }

        public async Task DeleteClaimMessages(Claim claim)
        {
            var claimDB = claimMapper.Map(claim);
            if (claimDB.ClaimMessages != null)
            {
                claimDB.ClaimMessages.ForEach(message =>
                {
                    applicationDbContext.ClaimMessages.Remove(message);
                    applicationDbContext.SaveChanges();
                });
            }
        }
    }
}
