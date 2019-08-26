using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimThirdInsuredVehicleDB : BaseEntityDB
    {
        public ClaimThirdInsuredVehicleDB()
        {
            Vehicle = new VehicleDB();
            Claim = new ClaimDB();
        }

        public static ClaimThirdInsuredVehicleDB NewInstance()
        {
            return new ClaimThirdInsuredVehicleDB();
        }

        public long VehicleId { get; set; }

        public VehicleDB Vehicle { get; set; }

        public long ClaimId { get; set; }

        public ClaimDB Claim { get; set; }
    }
}
