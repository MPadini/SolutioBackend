using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Solutio.Infrastructure.Repositories.Entities;
using Mapster;
using Solutio.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Solutio.Infrastructure.Repositories.Claims {
    public class UserRepository : IUserRepository {
        private readonly ApplicationDbContext applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<User>> GetAllUsers() {
            var users = applicationDbContext.Users.AsNoTracking().ToList().Where(x => x.IsEnabled);
            return users.Adapt<List<User>>();
        }
    }
}
