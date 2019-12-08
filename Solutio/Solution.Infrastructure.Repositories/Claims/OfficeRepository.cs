using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Solutio.Infrastructure.Repositories.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Core.Entities;

namespace Solutio.Infrastructure.Repositories.Claims {
    public class OfficeRepository : IOfficeRepository {
        private readonly ApplicationDbContext applicationDbContext;

        public OfficeRepository(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<Office>> GetAll() {
            var offices = await applicationDbContext.Offices.AsNoTracking().ToListAsync();

            return offices.Adapt<List<Office>>();
        }
    }
}
