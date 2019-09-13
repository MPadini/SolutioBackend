using Mapster;
using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Entities;
using Solutio.Infrastructure.Repositories.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimThirdInsuredPersonRepository : IClaimThirdInsuredPersonRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IClaimMapper claimMapper;

        public ClaimThirdInsuredPersonRepository(
            ApplicationDbContext applicationDbContext,
            IClaimMapper claimMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.claimMapper = claimMapper;
        }

        public async Task DeleteAll(Claim claim)
        {
            var claimDB = claimMapper.Map(claim);
            if (claimDB.ClaimThirdInsuredPersons == null) return;

            claimDB.ClaimThirdInsuredPersons.ForEach(async claimThirdInsuredPerson =>
            {
                await DeleteClaimPerson(claimThirdInsuredPerson);
            });
        }

        public async Task Delete(Claim claim, List<long> personIds)
        {
            var claimDB = claimMapper.Map(claim);
            if (claimDB.ClaimThirdInsuredPersons == null) return;
            if (personIds == null || !personIds.Any()) return;

            claimDB.ClaimThirdInsuredPersons.ForEach(async claimThirdInsuredPerson =>
            {
                if (personIds.Contains( claimThirdInsuredPerson.PersonId))
                {
                    await DeleteClaimPerson(claimThirdInsuredPerson);
                }
            });
        }

        private async Task DeleteClaimPerson(ClaimThirdInsuredPersonDB claimThirdInsuredPerson)
        {
            var person = claimThirdInsuredPerson.Person;
            claimThirdInsuredPerson.Claim = null;
            claimThirdInsuredPerson.Person = null;

            applicationDbContext.ClaimThirdInsuredPersons.Remove(claimThirdInsuredPerson);
            applicationDbContext.Persons.Remove(person);
            applicationDbContext.SaveChanges();
        }

        public async Task Save(Person person, long claimDbId)
        {
            var claimThirdInsured = ClaimThirdInsuredPersonDB.NewInstance();
            claimThirdInsured.Person = person.Adapt<PersonDB>();
            claimThirdInsured.ClaimId = claimDbId;
            claimThirdInsured.Claim = null;
            applicationDbContext.ClaimThirdInsuredPersons.Add(claimThirdInsured);
            applicationDbContext.SaveChanges();
        }

        public async Task Update(Person personNewData, Person existingPerson)
        {
            var personToUpdate = existingPerson.Adapt<PersonDB>();

            personToUpdate.Cuit = personNewData.Cuit;
            personToUpdate.DocumentNumber = personNewData.DocumentNumber;
            personToUpdate.Email = personNewData.Email;
            personToUpdate.LegalEntityName = personNewData.LegalEntityName;
            personToUpdate.MobileNumber = personNewData.MobileNumber;
            personToUpdate.Name = personNewData.Name;
            personToUpdate.PersonTypeId = personNewData.PersonTypeId;
            personToUpdate.Surname = personNewData.Surname;
            personToUpdate.TelephoneNumber = personNewData.TelephoneNumber;

            applicationDbContext.Persons.Update(personToUpdate);
            applicationDbContext.SaveChanges();
        }
    }
}
