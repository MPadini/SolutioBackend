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

        public async Task DeleteClaimThirdInsuredPersons(Claim claim)
        {
            var claimDB = claimMapper.Map(claim);
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

        public async Task<Claim> UpdateClaimThirdInsuredPersons(Claim claim, List<Person> persons)
        {
            var claimDb = claimMapper.Map(claim);
            if (claimDb.ClaimThirdInsuredPersons == null || persons == null) return default;

            persons.ForEach(person =>
            {
                var thirdInsuredPerson = claimDb.ClaimThirdInsuredPersons.FirstOrDefault(x => x.PersonId == person.Id);
                if (thirdInsuredPerson != null)
                {
                    thirdInsuredPerson.Person.Cuit = person.Cuit;
                    thirdInsuredPerson.Person.DocumentNumber = person.DocumentNumber;
                    thirdInsuredPerson.Person.Email = person.Email;
                    thirdInsuredPerson.Person.LegalEntityName = person.LegalEntityName;
                    thirdInsuredPerson.Person.MobileNumber = person.MobileNumber;
                    thirdInsuredPerson.Person.Name = person.Name;
                    thirdInsuredPerson.Person.PersonTypeId = person.PersonTypeId;
                    thirdInsuredPerson.Person.Surname = person.Surname;
                    thirdInsuredPerson.Person.TelephoneNumber = person.TelephoneNumber;

                    applicationDbContext.Persons.Update(thirdInsuredPerson.Person);
                    applicationDbContext.SaveChanges();
                }
                else
                {
                    var claimThirdInsured = ClaimThirdInsuredPersonDB.NewInstance();
                    claimThirdInsured.Person = person.Adapt<PersonDB>();
                    claimThirdInsured.ClaimId = claimDb.Id;
                    claimThirdInsured.Claim = null;
                    applicationDbContext.ClaimThirdInsuredPersons.Add(claimThirdInsured);
                    applicationDbContext.SaveChanges();
                }
            });

            return claimMapper.Map(claimDb);
        }
    }
}
