using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.AdressServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.AdressServices
{
    public class UpdateAdressService : IUpdateAdressService
    {
        private readonly IClaimAdressRepository claimAdressRepository;

        public UpdateAdressService(IClaimAdressRepository claimAdressRepository)
        {
            this.claimAdressRepository = claimAdressRepository;
        }

        public async Task<Adress> UpdateClaimAdress(Claim claim, Adress adress)
        {
            if (claim == null) return default;

            if (await AdressExists(claim))
            {
                return await claimAdressRepository.Update(claim, adress);
            }
            else
            {
                return await claimAdressRepository.Save(claim, adress);
            }
        }

        private async Task<bool> AdressExists(Claim claim)
        {
            if (claim.Adress != null)
            {
                return true;
            }

            return false;
        }
    }
}
