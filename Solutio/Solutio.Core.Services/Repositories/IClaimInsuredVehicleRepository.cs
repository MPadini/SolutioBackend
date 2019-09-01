using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimInsuredVehicleRepository
    {
        Task<Claim> UpdateClaimInsuredVehicles(Claim claim, List<Vehicle> vehicles);

        Task DeleteClaimInsuredVehicles(Claim claim);
    }
}
