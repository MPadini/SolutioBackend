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
            if(claim != null && persons != null)
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
    }
}
