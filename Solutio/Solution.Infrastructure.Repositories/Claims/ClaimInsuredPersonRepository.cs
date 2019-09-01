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
                claimDB.ClaimInsuredPersons.ForEach(person =>
                {
                    person.Claim = null;
                    person.Person = null;
                    applicationDbContext.ClaimInsuredPersons.Remove(person);
                    applicationDbContext.SaveChanges();
                });
            }
        }

        public async Task<Claim> UpdateClaimInsuredPersons(Claim claim, List<Person> persons)
        {
            var claimDb = claimMapper.Map(claim);
            if (claimDb.ClaimInsuredPersons == null || persons == null) return default;

            persons.ForEach(person =>
            {
                var insuredPerson = claimDb.ClaimInsuredPersons.FirstOrDefault(x => x.PersonId == person.Id);
                if (insuredPerson != null)
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
                }
                else
                {
                    var claimInsured = ClaimInsuredPersonDB.NewInstance();
                    claimInsured.Person = person.Adapt<PersonDB>();
                    claimInsured.ClaimId = claimDb.Id;
                    claimInsured.Claim = null;
                    applicationDbContext.ClaimInsuredPersons.Add(claimInsured);
                    applicationDbContext.SaveChanges();
                }
            });

            return claimMapper.Map(claimDb);
        }
    }
}
