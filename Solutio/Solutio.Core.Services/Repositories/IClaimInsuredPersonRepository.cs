using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimInsuredPersonRepository
    {
        Task<Claim> UpdateClaimInsuredPersons(Claim claim, List<Person> persons);

        Task DeleteClaimInsuredPersons(Claim claim);
    }
}
