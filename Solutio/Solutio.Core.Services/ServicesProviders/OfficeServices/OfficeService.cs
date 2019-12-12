using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.OfficeServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Office>> GetOfficesByUser(int userId) {
            return await officeRepository.GetOfficesByUser(userId);
        }

        public async Task SaveUserOffices(int userId, List<long> officesId) {
            if (officesId == null) return;
            if (!officesId.Any()) return;
            if (userId <= 0) return;

            List<UserOffice> userOffices = new List<UserOffice>();
            foreach (var officeId in officesId) {
                UserOffice userOffice = new UserOffice();
                userOffice.UserId = userId;
                userOffice.OfficeId = officeId;

                userOffices.Add(userOffice);
            }
           

            await officeRepository.SaveUserOffices(userOffices);
        }
    }
}
