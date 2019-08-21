﻿using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories.Location
{
    public interface IProvinceRepository
    {
        Task<List<Province>> GetByCountryId(long countryId);
    }
}
