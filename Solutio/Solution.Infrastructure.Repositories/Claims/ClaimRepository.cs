using Mapster;
using Microsoft.EntityFrameworkCore;
using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Entities;
using Solutio.Infrastructure.Repositories.Mappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IClaimMapper claimMapper;

        public ClaimRepository(ApplicationDbContext applicationDbContext, IClaimMapper claimMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.claimMapper = claimMapper;
        }

        public async Task<long> Save(Claim claim)
        {
            long result = 0;
            using (var transaction = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    var claimDb = claimMapper.Map(claim);

                    applicationDbContext.Claims.Add(claimDb);
                    applicationDbContext.SaveChanges();

                    transaction.Commit();

                    result = claimDb.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException(ex.Message);
                }
            }

            return result;
        }

        public async Task<Claim> GetById(long id)
        {
            var claimDb = await Get(id);
            return claimMapper.Map(claimDb);
        }

        private async Task<ClaimDB> Get(long id)
        {
            return await applicationDbContext.Claims.AsNoTracking()
                .Include(x => x.ClaimInsuredPersons).ThenInclude(x => x.Person)
                .Include(x => x.ClaimInsuredVehicles).ThenInclude(x => x.Vehicle)
                .Include(x => x.ClaimThirdInsuredPersons).ThenInclude(x => x.Person)
                .Include(x => x.ClaimThirdInsuredVehicles).ThenInclude(x => x.Vehicle)
                .Include(x => x.Files)
                .Include(x => x.Adress).ThenInclude(e => e.City)
                .Include(x => x.Adress).ThenInclude(e => e.Province)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Claim>> GetAll()
        {
            var claimsDb = await applicationDbContext.Claims.AsNoTracking().ToListAsync();

            return claimMapper.Map(claimsDb);
        }

        public async Task Update(Claim claim, long claimId)
        {
            using (var transaction = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    var claimDb = await Get(claimId);
                    if (claimDb == null) return;

                    await UpdateClaim(claimDb, claim);
                    await UpdateClaimAdress(claimDb, claim.Adress);

                    applicationDbContext.Claims.Update(claimDb);
                    applicationDbContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException(ex.Message);
                }
            }
        }

        private async Task<ClaimDB> UpdateClaim(ClaimDB claimDb, Claim claim)
        {
            claimDb.Story = claim.Story;
            claimDb.Hour = claim.Hour;
            claimDb.StateId = claim.StateId;
            claimDb.TotalBudgetAmount = claim.TotalBudgetAmount;
            claimDb.Date = claim.Date;

            return claimDb;
        }

        private async Task<ClaimDB> UpdateClaimAdress(ClaimDB claimDb, Adress adress)
        {
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

            return claimDb;
        }

        private async Task<bool> AdressExists(ClaimDB claimDb)
        {
            if (claimDb.Adress != null && claimDb.AdressId > 0)
            {
                return true;
            }

            return false;
        }

        public async Task Delete(Claim claim)
        {
            var claimDb = claimMapper.Map(claim);
            using (var transaction = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await DeleteClaimInsuredPersons(claimDb);
                    await DeleteClaimInsuredVehicles(claimDb);
                    await DeleteClaimThirdInsuredPersons(claimDb);
                    await DeleteClaimThirdInsuredVehicles(claimDb);
                    await DeleteClaimAdress(claimDb);
                    await DeleteClaimFiles(claimDb);

                    applicationDbContext.Claims.Remove(claimDb);
                    applicationDbContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException(ex.Message);
                }
            }
        }

        private async Task DeleteClaimInsuredPersons(ClaimDB claimDB)
        {
            if (claimDB.ClaimInsuredPersons != null)
            {
                claimDB.ClaimInsuredPersons.ForEach(person =>
                {
                    person.Claim = null;
                    person.Person = null;
                    applicationDbContext.ClaimInsuredPersons.Remove(person);
                    applicationDbContext.SaveChanges();
                });
            }
        }

        private async Task DeleteClaimThirdInsuredPersons(ClaimDB claimDB)
        {
            if (claimDB.ClaimThirdInsuredPersons != null)
            {
                claimDB.ClaimThirdInsuredPersons.ForEach(person =>
                {
                    person.Claim = null;
                    person.Person = null;
                    applicationDbContext.ClaimThirdInsuredPersons.Remove(person);
                    applicationDbContext.SaveChanges();
                });
            }
        }

        private async Task DeleteClaimInsuredVehicles(ClaimDB claimDB)
        {
            if (claimDB.ClaimInsuredVehicles != null)
            {
                claimDB.ClaimInsuredVehicles.ForEach(vehicle =>
                {
                    vehicle.Claim = null;
                    vehicle.Vehicle = null;
                    applicationDbContext.ClaimInsuredVehicles.Remove(vehicle);
                    applicationDbContext.SaveChanges();
                });
            }
        }

        private async Task DeleteClaimThirdInsuredVehicles(ClaimDB claimDB)
        {
            if (claimDB.ClaimThirdInsuredVehicles != null)
            {
                claimDB.ClaimThirdInsuredVehicles.ForEach(vehicle =>
                {
                    vehicle.Claim = null;
                    vehicle.Vehicle = null;
                    applicationDbContext.ClaimThirdInsuredVehicles.Remove(vehicle);
                    applicationDbContext.SaveChanges();
                });
            }
        }

        private async Task DeleteClaimFiles(ClaimDB claimDB)
        {
            if (claimDB.Files != null)
            {
                claimDB.Files.ForEach(file =>
                {
                    applicationDbContext.ClaimFiles.Remove(file);
                    applicationDbContext.SaveChanges();
                });
            }
        }

        private async Task DeleteClaimAdress(ClaimDB claimDB)
        {
            if (claimDB.Adress != null)
            {
                claimDB.Adress.Province = null;
                claimDB.Adress.City = null;
                applicationDbContext.Adresses.Remove(claimDB.Adress);
                applicationDbContext.SaveChanges();
            }
        }
    }
}
