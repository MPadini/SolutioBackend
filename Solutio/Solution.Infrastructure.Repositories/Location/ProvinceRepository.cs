using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories.Location;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using System.Linq;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Location
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProvinceRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<Province>> GetByCountryId(long countryId)
        {
            var provinces = applicationDbContext.Provinces.Where(x => x.CountryId == countryId);

            return provinces.Adapt<List<Province>>();
        }
    }
}
