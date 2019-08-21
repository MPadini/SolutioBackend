﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ClaimRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<long> Save(Claim claim)
        {
            long  result = 0;
            using (var transaction = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    var claimDb = claim.Adapt<ClaimDB>();

                    applicationDbContext.Claims.Add(claimDb);
                    applicationDbContext.SaveChanges();

                    transaction.Commit();

                    result = claimDb.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

            return result;
        }

        public async Task<Claim> GetById(long id)
        {
            var claimDb = await applicationDbContext.Claims.AsNoTracking()
                .Include(x => x.ClaimInsuredPersons)
                .ThenInclude(x => x.Person)
                .Include(x => x.ClaimInsuredVehicles)
                .ThenInclude(x => x.Vehicle)
                .Include(x => x.Files)
                .FirstOrDefaultAsync(x => x.Id == id);

            return claimDb.Adapt<Claim>();
        }

        public async Task<List<Claim>> GetAll()
        {
            var claimsDb = await applicationDbContext.Claims.AsNoTracking().ToListAsync();

            return claimsDb.Adapt<List<Claim>>();
        }

        public Task Update(Claim claim)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Claim claim)
        {
            var claimDb = claim.Adapt<ClaimDB>();
            using (var transaction = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await DeleteClaimInsuredPersons(claimDb);
                    await DeleteClaimInsuredVehicles(claimDb);
                    await DeleteClaimFiles(claimDb);

                    applicationDbContext.Claims.Remove(claimDb);
                    applicationDbContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
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
    }
}