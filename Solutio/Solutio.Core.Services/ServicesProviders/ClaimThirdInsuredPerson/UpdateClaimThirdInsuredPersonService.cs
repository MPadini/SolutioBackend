using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimThirdInsuredPerson;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimThirdInsuredPerson
{
    public class UpdateClaimThirdInsuredPersonService : IUpdateClaimThirdInsuredPersonService
    {
        private readonly IClaimThirdInsuredPersonRepository claimThirdInsuredPersonRepository;

        public UpdateClaimThirdInsuredPersonService(IClaimThirdInsuredPersonRepository claimThirdInsuredPersonRepository)
        {
            this.claimThirdInsuredPersonRepository = claimThirdInsuredPersonRepository;
        }

        public async Task UpdateClaimThirdInsuredPersons(Claim claim, List<Person> persons)
        {
            if (claim == null) return;

            await UpdateOrSavePersons(claim, persons);
            await DeletePersons(claim, persons);
        }

        private async Task UpdateOrSavePersons(Claim claim, List<Person> persons)
        {
            if (persons != null)
            {
                persons.ForEach(async person =>
                {
                    var existingPerson = claim.ClaimThirdInsuredPersons.FirstOrDefault(x => x.Id == person.Id);
                    if (existingPerson != null)
                    {
                        await claimThirdInsuredPersonRepository.Update(person, existingPerson);
                    }
                    else
                    {
                        await claimThirdInsuredPersonRepository.Save(person, claim.Id);
                    }
                });
            }
        }

        private async Task DeletePersons(Claim claim, List<Person> persons)
        {
            if (claim.ClaimThirdInsuredPersons == null) return;
            
            if (persons == null || !persons.Any())
            {
                await claimThirdInsuredPersonRepository.DeleteAll(claim);
            }
            else
            {
                var PersonsToDelete = claim.ClaimThirdInsuredPersons.Select(x => x.Id)
                    .Except(persons.Select(x => x.Id))
                    .ToList();

                if (PersonsToDelete != null && PersonsToDelete.Any())
                {
                    await claimThirdInsuredPersonRepository.Delete(claim, PersonsToDelete);
                }
            }
        }
    }
}
