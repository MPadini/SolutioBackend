﻿using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories.ClaimsRepositories
{
    public interface IClaimRepository
    {
        Task<long> Save(Claim claim);

        Task<Claim> GetById(long id);

        Task<List<Claim>> GetAll();

        Task Update(Claim claim);

        Task Delete(Claim claim);
    }
}
