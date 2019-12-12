using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.AdressServices {
    public interface ICreateAdressService {

        Task<long> Save(Adress adress);
    }
}
