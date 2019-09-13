using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimThirdInsuredPerson
{
    public interface IUpdateClaimThirdInsuredPersonService
    {
        Task UpdateClaimThirdInsuredPersons(Claim claim, List<Person> persons);
    }
}
