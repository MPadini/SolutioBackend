using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimInsuredVehicle
    {
        public ClaimInsuredVehicle()
        {
            Vehicle = new Vehicle();
        }

        public long VehicleId { get; set; }

        public long ClaimId { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
