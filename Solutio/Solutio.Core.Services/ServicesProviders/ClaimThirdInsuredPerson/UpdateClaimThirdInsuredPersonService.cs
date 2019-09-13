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
            if (claim != null && persons != null)
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
    }
}
