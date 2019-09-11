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

                    claimDb.StateModifiedDate = DateTime.Now;

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
            try
            {
                var claimDb = claimMapper.Map(claim);

                applicationDbContext.Claims.Update(await SetClaimNull(claimDb));
                applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task UpdateState(Claim claim, long claimId)
        {
            using (var transaction = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    var claimDb = await Get(claimId);
                    if (claimDb == null) return;

                    claimDb.StateId = claim.StateId;
                    claimDb.StateModifiedDate = claim.StateModifiedDate;

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

        private async Task DeleteAssociatedEntities(Claim claim)
        {
            await claimInsuredPersonRepository.Delete(claim);
            await claimThirdInsuredVehicleRepository.DeleteClaimThirdInsuredVehicles(claim);
            await claimInsuredVehicleRepository.DeleteClaimInsuredVehicles(claim);
            await claimThirdInsuredPersonRepository.DeleteClaimThirdInsuredPersons(claim);  
            await claimFileRepository.DeleteClaimFiles(claim);
            await claimAdressRepository.Delete(claim);
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
    }
}
