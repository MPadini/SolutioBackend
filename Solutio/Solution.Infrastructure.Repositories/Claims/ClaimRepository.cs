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
using System.Linq;
using Solutio.Core.Services.Repositories;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IClaimMapper claimMapper;
        private readonly IClaimThirdInsuredVehicleRepository claimThirdInsuredVehicleRepository;
        private readonly IClaimThirdInsuredPersonRepository claimThirdInsuredPersonRepository;
        private readonly IClaimInsuredVehicleRepository claimInsuredVehicleRepository;
        private readonly IClaimInsuredPersonRepository claimInsuredPersonRepository;
        private readonly IClaimAdressRepository claimAdressRepository;
        private readonly IClaimFileRepository claimFileRepository;

        public ClaimRepository(
            ApplicationDbContext applicationDbContext,
            IClaimMapper claimMapper,
            IClaimThirdInsuredVehicleRepository claimThirdInsuredVehicleRepository,
            IClaimThirdInsuredPersonRepository claimThirdInsuredPersonRepository,
            IClaimInsuredVehicleRepository claimInsuredVehicleRepository,
            IClaimInsuredPersonRepository claimInsuredPersonRepository,
            IClaimAdressRepository claimAdressRepository,
            IClaimFileRepository claimFileRepository)
        {
            this.applicationDbContext = applicationDbContext;
            this.claimMapper = claimMapper;
            this.claimThirdInsuredVehicleRepository = claimThirdInsuredVehicleRepository;
            this.claimThirdInsuredPersonRepository = claimThirdInsuredPersonRepository;
            this.claimInsuredVehicleRepository = claimInsuredVehicleRepository;
            this.claimInsuredPersonRepository = claimInsuredPersonRepository;
            this.claimAdressRepository = claimAdressRepository;
            this.claimFileRepository = claimFileRepository;
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

        public async Task<List<Claim>> GetAll()
        {
            var claimsDb = await applicationDbContext.Claims.AsNoTracking()
                .Include(x => x.State)
                .ThenInclude(e => e.StateConfigurations)
                .ThenInclude(d => d.AllowedState)
                .ToListAsync();

            return claimMapper.Map(claimsDb);
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
                .Include(x => x.State).ThenInclude(e => e.StateConfigurations).ThenInclude(d => d.AllowedState)
                .FirstOrDefaultAsync(x => x.Id == id);
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
                    await UpdateAssociatedEntities(claimDb, claim);

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

        private async Task UpdateAssociatedEntities(ClaimDB claimDB, Claim claim)
        {
            var existingClaim = claimMapper.Map(claimDB);
            await claimInsuredPersonRepository.UpdateClaimInsuredPersons(existingClaim, claim.ClaimInsuredPersons);
            await claimInsuredVehicleRepository.UpdateClaimInsuredVehicles(existingClaim, claim.ClaimInsuredVehicles);
            await claimThirdInsuredPersonRepository.UpdateClaimThirdInsuredPersons(existingClaim, claim.ClaimThirdInsuredPersons);
            await claimThirdInsuredVehicleRepository.UpdateClaimThirdInsuredVehicles(existingClaim, claim.ClaimThirdInsuredVehicles);
        }

        private async Task DeleteAssociatedEntities(Claim claim)
        {
            await claimInsuredPersonRepository.DeleteClaimInsuredPersons(claim);
            await claimThirdInsuredVehicleRepository.DeleteClaimThirdInsuredVehicles(claim);
            await claimInsuredVehicleRepository.DeleteClaimInsuredVehicles(claim);
            await claimThirdInsuredPersonRepository.DeleteClaimThirdInsuredPersons(claim);
            
            await claimFileRepository.DeleteClaimFiles(claim);
            await claimAdressRepository.DeleteClaimAdress(claim);
        }

        public async Task Delete(Claim claim)
        {
            var claimDb = claimMapper.Map(claim);
            using (var transaction = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await DeleteAssociatedEntities(claim);

                    applicationDbContext.Claims.Remove(await SetClaimNull(claimDb));
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

        private async Task<ClaimDB> SetClaimNull(ClaimDB claimDB)
        {
            claimDB.ClaimInsuredPersons = null;
            claimDB.ClaimInsuredVehicles = null;
            claimDB.ClaimThirdInsuredPersons = null;
            claimDB.ClaimThirdInsuredVehicles = null;
            claimDB.Adress = null;
            return claimDB;
        }

        private async Task<ClaimDB> UpdateClaim(ClaimDB claimDb, Claim claim)
        {
            claimDb.Story = claim.Story;
            claimDb.Hour = claim.Hour;
            claimDb.TotalBudgetAmount = claim.TotalBudgetAmount;
            claimDb.Date = claim.Date;
            var adress = await claimAdressRepository.UpdateClaimAdress(claimDb.Adapt<Claim>(), claim.Adress);
            claimDb.AdressId = adress.Id;
            return claimDb;
        }


    }
}
