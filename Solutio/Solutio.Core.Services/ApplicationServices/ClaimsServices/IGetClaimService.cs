using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimsServices
{
    public interface IGetClaimService
    {
        Task<Claim> GetById(long id);

        Task<List<Claim>> GetAll(string userName, long officeId);

        Task<List<Claim>> GetClaimByInsuranceCompany(long insuranceCompany);
    }
}
