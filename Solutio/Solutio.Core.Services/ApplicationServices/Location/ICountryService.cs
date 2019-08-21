﻿using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.Location
{
    public interface ICountryService
    {
        Task<List<Country>> GetAll();
    }
}
