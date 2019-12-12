using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.AdressServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.AdressServices {
    public class CreateAdressService : ICreateAdressService {
        private readonly IClaimAdressRepository claimAdressRepository;

        public CreateAdressService(IClaimAdressRepository claimAdressRepository) {
            this.claimAdressRepository = claimAdressRepository;
        }

        public async Task<long> Save(Adress adress) {
            return await claimAdressRepository.Save(adress);
        }
    }
}
