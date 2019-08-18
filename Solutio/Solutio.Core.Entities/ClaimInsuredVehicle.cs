using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimInsuredVehicle
    {
        public long VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        public long ClaimId { get; set; }

        public Claim Claim { get; set; }

       // public long VehicleParticipationTypeId { get; set; }

       // public VehicleParticipationType VehicleParticipationType { get; set; }
    }
}
