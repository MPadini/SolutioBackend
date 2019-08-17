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

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimFileRepository : IClaimFileRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ClaimFileRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<long> Upload(ClaimFile file)
        {
            try
            {
                var fileDb = file.Adapt<ClaimFileDB>();

                applicationDbContext.Add(fileDb);
                applicationDbContext.SaveChanges();

                return fileDb.Id;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task Delete(ClaimFile file)
        {
            var fileDb = file.Adapt<ClaimFileDB>();

            applicationDbContext.Remove(fileDb);
            applicationDbContext.SaveChanges();
        }

        public async Task<ClaimFile> GetById(long fileId)
        {
            var file = applicationDbContext.ClaimFiles.AsNoTracking().FirstOrDefault(x => x.Id == fileId);

            return file.Adapt<ClaimFile>();
        }
    }
}
