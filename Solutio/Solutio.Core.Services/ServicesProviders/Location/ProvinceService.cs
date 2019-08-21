using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.Location;
using Solutio.Core.Services.Repositories.Location;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.Location
{
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepository provinceRepository;

        public ProvinceService(IProvinceRepository provinceRepository)
        {
            this.provinceRepository = provinceRepository;
        }

        public async Task<List<Province>> GetByCountryId(long countryId)
        {
            return await provinceRepository.GetByCountryId(countryId);
        }
    }
}
