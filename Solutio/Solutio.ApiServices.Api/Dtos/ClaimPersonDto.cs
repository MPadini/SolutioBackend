using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class ClaimPersonDto
    {
        public long PersonResponsabilityTypeId { get; set; }

        public PersonResponsabilityTypeDto PersonResponsabilityType { get; set; }

        public long PersonId { get; set; }

        public PersonDto Person { get; set; }

        public long ClaimId { get; set; }

        public ClaimDto Claim { get; set; }
    }
}
