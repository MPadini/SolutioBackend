using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimPersonServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimPersonServices
{
    public class UpdateClaimInsuredPersonService : IUpdateClaimInsuredPersonService
    {
        private readonly IClaimInsuredPersonRepository claimInsuredPersonRepository;

        public UpdateClaimInsuredPersonService(IClaimInsuredPersonRepository claimInsuredPersonRepository)
        {
            this.claimInsuredPersonRepository = claimInsuredPersonRepository;
        }

        public async Task UpdateClaimInsuredPersons(Claim claim, List<Person> persons)
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
                    var existingPerson = claim.ClaimInsuredPersons.FirstOrDefault(x => x.Id == person.Id);
                    if (existingPerson != null)
                    {
                        await claimInsuredPersonRepository.Update(person, existingPerson);
                    }
                    else
                    {
                        await claimInsuredPersonRepository.Save(person, claim.Id);
                    }
                });
            }
        }

        private async Task DeletePersons(Claim claim, List<Person> persons)
        {
            if (claim.ClaimInsuredPersons == null) return;

            if (persons == null || !persons.Any())
            {
                await claimInsuredPersonRepository.DeleteAll(claim);
            }
            else
            {
                var PersonsToDelete = claim.ClaimInsuredPersons.Select(x => x.Id)
                    .Except(persons.Select(x => x.Id))
                    .ToList();

                if (PersonsToDelete != null && PersonsToDelete.Any())
                {
                    await claimInsuredPersonRepository.Delete(claim, PersonsToDelete);
                }
            }
        }
    }
}
