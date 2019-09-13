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

        public async Task DeleteAll(Claim claim)
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

        public async Task Save(Person person, long claimDbId)
        {
            var claimInsured = ClaimInsuredPersonDB.NewInstance();
            claimInsured.Person = person.Adapt<PersonDB>();
            claimInsured.ClaimId = claimDbId;
            claimInsured.Claim = null;
            applicationDbContext.ClaimInsuredPersons.Add(claimInsured);
            applicationDbContext.SaveChanges();
        }

        private async Task DeleteClaimInsuredPersons(Claim claim, List<Person> persons)
        {
            //var claimPersonToDelete = claimDb.ClaimInsuredPersons.Where((n) => !persons.Contains(n.Person.Adapt<Person>())).ToList();
            //if (claimPersonToDelete != null)
            //{
            //    claimPersonToDelete.ForEach(async claimPerson => await DeleteClaimPerson(claimPerson));
            //}
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
