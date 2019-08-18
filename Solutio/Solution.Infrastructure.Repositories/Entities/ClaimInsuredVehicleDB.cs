using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimInsuredVehicleDB : BaseEntityDB
    {
        public ClaimInsuredVehicleDB()
        {
            Vehicle = new VehicleDB();
            Claim = new ClaimDB();
        }

        public long VehicleId { get; set; }

        public VehicleDB Vehicle { get; set; }

        public long ClaimId { get; set; }

        public ClaimDB Claim { get; set; }
    }
}
