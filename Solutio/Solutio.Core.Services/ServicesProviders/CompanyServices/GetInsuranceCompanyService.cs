using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.CompanyServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.CompanyServices {
    public class GetInsuranceCompanyService : IGetInsuranceCompanyService {
        private readonly IInsuranceCompanyRepository insuranceCompanyRepository;

        public GetInsuranceCompanyService(IInsuranceCompanyRepository insuranceCompanyRepository) {
            this.insuranceCompanyRepository = insuranceCompanyRepository;
        }

        public async Task<List<InsuranceCompany>> GetCompanies() {
            return await insuranceCompanyRepository.GetCompanies();
        }
    }
}
