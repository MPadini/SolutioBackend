using Microsoft.EntityFrameworkCore;
using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace Solutio.Infrastructure.Repositories.Claims {
    public class ClaimDocumentTemplateRepository : IClaimDocumentTemplateRepository {
        private readonly ApplicationDbContext applicationDbContext;

        public ClaimDocumentTemplateRepository(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<ClaimDocument>> GetTemplates() {
            var result = applicationDbContext.ClaimDocuments.AsNoTracking().ToList();

            return result.Adapt<List<ClaimDocument>>();
        }
    }
}
