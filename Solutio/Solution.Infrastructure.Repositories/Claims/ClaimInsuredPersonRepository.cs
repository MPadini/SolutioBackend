using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Mappers;
using System.Linq;
using Solutio.Infrastructure.Repositories.Entities;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimInsuredPersonRepository : IClaimInsuredPersonRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IClaimMapper claimMapper;

        public ClaimInsuredPersonRepository(
            ApplicationDbContext applicationDbContext,
            IClaimMapper claimMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.claimMapper = claimMapper;
        }

        public async Task DeleteClaimInsuredPersons(Claim claim)
        {
            var claimDB = claimMapper.Map(claim);
            if (claimDB.ClaimInsuredPersons != null)
            {
                claimDB.ClaimInsuredPersons.ForEach(async person =>
                {
                    await DeleteClaimPerson(person);
                });
            }
        }

        private async Task DeleteClaimPerson(ClaimInsuredPersonDB claimInsuredPerson)
        {
            var person = claimInsuredPerson.Person;
            claimInsuredPerson.Claim = null;
            claimInsuredPerson.Person = null;
           
            applicationDbContext.ClaimInsuredPersons.Remove(claimInsuredPerson);
            applicationDbContext.Persons.Remove(person);
            applicationDbContext.SaveChanges();
        }

        private async Task Update(Person person, ClaimInsuredPersonDB insuredPerson)
        {
            insuredPerson.Person.Cuit = person.Cuit;
            insuredPerson.Person.DocumentNumber = person.DocumentNumber;
            insuredPerson.Person.Email = person.Email;
            insuredPerson.Person.LegalEntityName = person.LegalEntityName;
            insuredPerson.Person.MobileNumber = person.MobileNumber;
            insuredPerson.Person.Name = person.Name;
            insuredPerson.Person.PersonTypeId = person.PersonTypeId;
            insuredPerson.Person.Surname = person.Surname;
            insuredPerson.Person.TelephoneNumber = person.TelephoneNumber;

            applicationDbContext.Persons.Update(insuredPerson.Person);
            applicationDbContext.SaveChanges();
        }

        private async Task Save(Person person, long claimDbId)
        {
            var claimInsured = ClaimInsuredPersonDB.NewInstance();
            claimInsured.Person = person.Adapt<PersonDB>();
            claimInsured.ClaimId = claimDbId;
            claimInsured.Claim = null;
            applicationDbContext.ClaimInsuredPersons.Add(claimInsured);
            applicationDbContext.SaveChanges();
        }

        public async Task<Claim> UpdateClaimInsuredPersons(Claim claim, List<Person> persons)
        {
            var claimDb = claimMapper.Map(claim);
            if (claimDb.ClaimInsuredPersons == null || persons == null) return default;

            //var claimPersonToDelete = claimDb.ClaimInsuredPersons.Where((n) => !persons.Contains(n.Person.Adapt<Person>())).ToList();
            //if (claimPersonToDelete != null)
            //{
            //    claimPersonToDelete.ForEach(async claimPerson => await DeleteClaimPerson(claimPerson));
            //}

            persons.ForEach(async person =>
            {
                var insuredPerson = claimDb.ClaimInsuredPersons.FirstOrDefault(x => x.PersonId == person.Id);
                if (insuredPerson != null)
                {
                    await Update(person, insuredPerson);
                }
                else
                {
                    await Save(person, claimDb.Id);
                }
            });

            return claimMapper.Map(claimDb);
        }
    }
}
