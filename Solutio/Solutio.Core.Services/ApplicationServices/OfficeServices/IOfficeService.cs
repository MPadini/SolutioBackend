using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.OfficeServices {
    public interface IOfficeService {

        Task<List<Office>> GetAll();

        Task SaveUserOffices(int userId, List<long> officesId);
    }
}
