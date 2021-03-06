﻿using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimVehicleServices
{
    public interface IUpdateClaimInsuredVehicleService
    {
        Task UpdateClaimInsuredVehicles(Claim claim, List<Vehicle> vehicles);
    }
}
