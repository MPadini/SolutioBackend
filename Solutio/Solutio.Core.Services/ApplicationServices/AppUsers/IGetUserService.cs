using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.AppUsers {
    public interface IGetUserService {

        Task<List<User>> GetAllUsers();
    }
}
