using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories.Location;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Location
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CountryRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<Country>> GetAll()
        {
            var countries = applicationDbContext.Countries.ToList();

            return countries.Adapt<List<Country>>();
        }
    }
}
