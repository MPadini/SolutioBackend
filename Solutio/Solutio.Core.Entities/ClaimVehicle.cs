using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimVehicle
    {
        public long VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        public long ClaimId { get; set; }

        public Claim Claim { get; set; }
    }
}
