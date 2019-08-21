using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.Location;
using Solutio.Core.Services.Repositories.Location;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.Location
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task<List<Country>> GetAll()
        {
            return await countryRepository.GetAll();
        }
    }
}
