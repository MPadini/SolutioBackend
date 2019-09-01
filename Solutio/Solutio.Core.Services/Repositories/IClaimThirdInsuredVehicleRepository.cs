using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimThirdInsuredVehicleRepository
    {
        Task<Claim> UpdateClaimThirdInsuredVehicles(Claim claim, List<Vehicle> vehicles);

        Task DeleteClaimThirdInsuredVehicles(Claim claim);
    }
}
