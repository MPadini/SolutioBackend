using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Solutio.Infrastructure.Repositories.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Core.Entities;

namespace Solutio.Infrastructure.Repositories.Claims {
    public class OfficeRepository : IOfficeRepository {
        private readonly ApplicationDbContext applicationDbContext;

        public OfficeRepository(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<Office>> GetAll() {
            var offices = await applicationDbContext.Offices.AsNoTracking().ToListAsync();

            return offices.Adapt<List<Office>>();
        }

        public async Task<List<Office>> GetOfficesByUser(int userId) {
           var userOffices =  applicationDbContext.AspNetUserOffices.AsNoTracking().Where(x => x.UserId == userId).ToList();

            if (userOffices == null) return null;
            List<long> officesId = new List<long>();
            foreach(var userOffice in userOffices) {
                officesId.Add(userOffice.OfficeId);
            }

            var offices = applicationDbContext.Offices.Where(x => officesId.Contains(x.Id)).ToList();

            return offices.Adapt<List<Office>>();
        }

        public async Task SaveUserOffices(List<UserOffice> userOffices) {
            if (userOffices == null) return;
            if (!userOffices.Any()) return;

            var mappedUserOffices = userOffices.Adapt<List<UserOfficeDB>>();

            applicationDbContext.AspNetUserOffices.AddRange(mappedUserOffices);
            applicationDbContext.SaveChanges();
        }
    }
}
