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

        public async Task<long> Save(Claim claim, string userName)
        {
            long result = 0;
            using (var transaction = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    var claimDb = claimMapper.Map(claim);

                    claimDb.StateModifiedDate = DateTime.Now;
                    claimDb.UserName = userName;

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
                .Include(x => x.ClaimInsuredPersons)
                .ThenInclude(x => x.Person)
                .Include(x => x.ClaimInsuredVehicles)
                .ThenInclude(x => x.Vehicle)
                .Include(x => x.ClaimThirdInsuredVehicles)
                .ThenInclude(x => x.Vehicle)
                .ThenInclude(x => x.InsuranceCompany)
                .ToListAsync();

            return claimMapper.Map(claimsDb);
        }

        public async Task<List<Claim>> GetClaimByInsuranceCompany(long insuranceCompany)
        {
            var claimsDb = applicationDbContext.Claims.AsNoTracking().FromSql($"SELECT c.* FROM [dbo].[Vehicles] V  INNER JOIN [dbo].[ClaimThirdInsuredVehicles] " + "" +
                $"IV ON IV.VehicleId = V.Id INNER JOIN [dbo].[Claims] C ON C.Id = IV.ClaimId where V.InsuranceCompanyId = {insuranceCompany} " +
                "and c.StateId in (21, 22, 23, 32, 33, 41, 43, 44) ").ToList();
            if (claimsDb == null) return null;

            List<long> claimsId = new List<long>();
            claimsId.AddRange(claimsDb.Select(x => x.Id));

            var claims = applicationDbContext.Claims.AsNoTracking()
              .Include(x => x.ClaimInsuredPersons).ThenInclude(x => x.Person)
              .Include(x => x.ClaimInsuredVehicles).ThenInclude(x => x.Vehicle)
              .Include(x => x.ClaimThirdInsuredPersons).ThenInclude(x => x.Person)
              .Include(x => x.ClaimThirdInsuredVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.InsuranceCompany)
              .Include(x => x.State).ThenInclude(e => e.StateConfigurations).ThenInclude(d => d.AllowedState)
              .Where(x => claimsId.Contains(x.Id)).ToList();

            return claimMapper.Map(claims);
        }

        public async Task<List<Claim>> GetClaimByState(long stateId)
        {
            var claimsDB = applicationDbContext.Claims.AsNoTracking().Where(x => x.StateId == stateId).ToList();

            return claimMapper.Map(claimsDB);
        }


        private async Task<ClaimDB> Get(long id)
        {
            try
            {
                return await applicationDbContext.Claims.AsNoTracking()
               .Include(x => x.ClaimInsuredPersons).ThenInclude(x => x.Person)
               .Include(x => x.ClaimOffers)
               .Include(x => x.ClaimInsuredVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleType)
               .Include(x => x.ClaimThirdInsuredPersons).ThenInclude(x => x.Person)
               .Include(x => x.ClaimThirdInsuredVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.InsuranceCompany)
               .Include(x => x.ClaimThirdInsuredVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleType)
               .Include(x => x.Adress).ThenInclude(e => e.City)
               .Include(x => x.Adress).ThenInclude(e => e.Province)
               .Include(x => x.State).ThenInclude(e => e.StateConfigurations).ThenInclude(d => d.AllowedState)
               .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {

                throw;
            }

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
            //using (var transaction = applicationDbContext.Database.BeginTransaction())
            //{
            try
            {
                var claimDb = await Get(claimId);
                if (claimDb == null) return;

                claimDb.State = null;
                claimDb.StateId = claim.StateId;
                claimDb.StateModifiedDate = claim.StateModifiedDate;

                applicationDbContext.Claims.Update(claimDb);
                applicationDbContext.SaveChanges();

                //  transaction.Commit();
            }
            catch (Exception ex)
            {
                //  transaction.Rollback();
                throw new ApplicationException(ex.Message);
            }
            // }
        }

        private async Task DeleteAssociatedEntities(Claim claim)
        {
            await claimInsuredPersonRepository.DeleteAll(claim);
            await claimThirdInsuredVehicleRepository.DeleteAll(claim);
            await claimInsuredVehicleRepository.DeleteAll(claim);
            await claimThirdInsuredPersonRepository.DeleteAll(claim);
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
            claimDB.Files = null;
            claimDB.Adress = null;
            claimDB.ClaimOffers = null;
            return claimDB;
        }

        public async Task MarkAsPrinted(List<Claim> claims)
        {
            if (claims == null) return;
            if (!claims.Any()) return;

            foreach (var claim in claims)
            {
                applicationDbContext.Database.ExecuteSqlCommand("update claims set printed = 1 where id = {0}", claim.Id);
            }
        }
    }
}
