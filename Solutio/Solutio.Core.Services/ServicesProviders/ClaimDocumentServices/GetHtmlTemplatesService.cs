using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimDocumentServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimDocumentServices {
    public class GetHtmlTemplatesService : IGetHtmlTemplatesService {
        private readonly IClaimDocumentTemplateRepository claimDocumentTemplateRepository;

        public GetHtmlTemplatesService(IClaimDocumentTemplateRepository claimDocumentTemplateRepository) {
            this.claimDocumentTemplateRepository = claimDocumentTemplateRepository;
        }

        public async Task<List<ClaimDocument>> GetHtmlTemplates() {
            return await claimDocumentTemplateRepository.GetTemplates();
        }
    }
}
