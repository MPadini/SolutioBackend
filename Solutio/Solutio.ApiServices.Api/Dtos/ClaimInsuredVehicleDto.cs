using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class ClaimInsuredVehicleDto
    {
        //public long VehicleId { get; set; }

        public VehicleDto Vehicle { get; set; }

        //public long ClaimId { get; set; }

        public ClaimDto Claim { get; set; }
    }
}
