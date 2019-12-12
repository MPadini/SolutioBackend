using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories {
    public interface IOfficeRepository {

        Task<List<Office>> GetAll();

        Task SaveUserOffices(List<UserOffice> userOffices);

        Task<List<Office>> GetOfficesByUser(int userId);
    }
}
