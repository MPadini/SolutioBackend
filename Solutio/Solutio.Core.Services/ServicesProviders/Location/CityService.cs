using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.Location;
using Solutio.Core.Services.Repositories.Location;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.Location
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public async Task<List<City>> GetByProvinceId(long provinceId)
        {
            return await cityRepository.GetByProvinceId(provinceId);
        }
    }
}
