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
    public class InsuranceCompanyRepository : IInsuranceCompanyRepository {
        private readonly ApplicationDbContext applicationDbContext;

        public InsuranceCompanyRepository(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<InsuranceCompany>> GetCompanies() {
            var result = applicationDbContext.InsuranceCompanies.AsNoTracking().ToList();

            return result.Adapt<List<InsuranceCompany>>();
        }
    }
}
