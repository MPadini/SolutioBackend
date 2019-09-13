using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimInsuredVehicleRepository
    {
        Task DeleteAll(Claim claim);

        Task Delete(Claim claim, List<long> vehicleIds);

        Task Save(Vehicle vehicle, long claimDbId);

        Task Update(Vehicle existingVehicle, Vehicle vehicleNewData);
    }
}
