using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimInsuredVehicleRepository
    {
        Task Delete(Claim claim);

        Task Save(Vehicle vehicle, long claimDbId);

        Task Update(Vehicle existingVehicle, Vehicle vehicleNewData);
    }
}
