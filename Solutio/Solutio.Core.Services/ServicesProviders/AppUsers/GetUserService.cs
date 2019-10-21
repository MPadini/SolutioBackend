using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.AppUsers;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.AppUsers {
    public class GetUserService : IGetUserService {
        private readonly IUserRepository userRepository;

        public GetUserService(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers() {
            return await userRepository.GetAllUsers();
        }
    }
}
