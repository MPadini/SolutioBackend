using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimInsuredPersonRepository
    {
        Task DeleteAll(Claim claim);

        Task Delete(Claim claim, List<long> personIds);

        Task Update(Person personNewData, Person existingPerson);

        Task Save(Person person, long claimDbId);
    }
}
