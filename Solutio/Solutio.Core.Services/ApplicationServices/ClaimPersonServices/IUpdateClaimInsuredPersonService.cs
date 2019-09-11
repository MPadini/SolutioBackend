using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimPersonServices
{
    public interface IUpdateClaimInsuredPersonService
    {
        Task UpdateClaimInsuredPersons(Claim claim, List<Person> persons);
    }
}
