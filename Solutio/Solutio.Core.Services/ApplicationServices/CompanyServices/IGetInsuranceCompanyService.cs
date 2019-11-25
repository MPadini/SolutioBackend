using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.CompanyServices {
    public interface IGetInsuranceCompanyService {

        Task<List<InsuranceCompany>> GetCompanies();

        Task<List<InsuranceCompanyClaims>> GetInsuranceCompanyClaims();

    }
}
