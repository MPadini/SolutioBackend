using Mapster;
using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Entities;
using Solutio.Infrastructure.Repositories.Mappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimAdressRepository : IClaimAdressRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IClaimMapper claimMapper;

        public ClaimAdressRepository(
            ApplicationDbContext applicationDbContext,
            IClaimMapper claimMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.claimMapper = claimMapper;
        }

        public async Task DeleteClaimAdress(Claim claim)
        {
            var claimDB = claimMapper.Map(claim);
            if (claimDB.Adress != null)
            {
                claimDB.Adress.Province = null;
                claimDB.Adress.City = null;
                applicationDbContext.Adresses.Remove(claimDB.Adress);
                applicationDbContext.SaveChanges();
            }
        }

        public async Task<Claim> UpdateClaimAdress(Claim claim, Adress adress)
        {
            var claimDb = claimMapper.Map(claim);
            if (adress == null || claimDb == null) return default;

            if (await AdressExists(claimDb))
            {
                claimDb.Adress.Intersection = adress.Intersection;
                claimDb.Adress.Street = adress.Street;
                claimDb.Adress.Number = adress.Number;
                claimDb.Adress.CityId = adress.CityId;
                claimDb.Adress.ProvinceId = adress.ProvinceId;
            }
            else
            {
                var adressDb = adress.Adapt<AdressDB>();
                applicationDbContext.Adresses.Add(adressDb);
                claimDb.Adress = adressDb;
                applicationDbContext.SaveChanges();
            }

            return claimMapper.Map(claimDb);
        }

        private async Task<bool> AdressExists(ClaimDB claimDb)
        {
            if (claimDb.Adress != null && claimDb.AdressId > 0)
            {
                return true;
            }

            return false;
        }
    }
}
