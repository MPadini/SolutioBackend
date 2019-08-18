using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimInsuredVehicleDB : BaseEntityDB
    {
        public long VehicleId { get; set; }

        public VehicleDB Vehicle { get; set; }

        public long ClaimId { get; set; }

        public ClaimDB Claim { get; set; }

        //public long VehicleParticipationTypeId { get; set; }

        //public VehicleParticipationTypeDB VehicleParticipationType { get; set; }
    }
}
