using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.OfficeServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.OfficeServices {
    public class OfficeService : IOfficeService {
        private readonly IOfficeRepository officeRepository;

        public OfficeService(IOfficeRepository officeRepository) {
            this.officeRepository = officeRepository;
        }

        public async Task<List<Office>> GetAll() {
            return await officeRepository.GetAll();
        }
    }
}
