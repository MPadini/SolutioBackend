using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories.ClaimsRepositories
{
    public interface IClaimRepository
    {
        Task<long> Save(Claim claim, string userName);

        Task<Claim> GetById(long id);

        Task<List<Claim>> GetAll();

        Task Update(Claim claim, long claimId);

        Task UpdateState(Claim claim, long claimId);

        Task Delete(Claim claim);

        Task<List<Claim>> GetClaimByInsuranceCompany(long insuranceCompany);

        Task MarkAsPrinted(List<Claim> claims);

        Task<List<Claim>> GetClaimByState(long stateId);
    }
}
