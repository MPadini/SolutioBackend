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
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CityRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<City>> GetByProvinceId(long provinceId)
        {
            var cities = applicationDbContext.Cities.Where(x => x.ProvinceId == provinceId);

            return cities.Adapt<List<City>>();
        }
    }
}
