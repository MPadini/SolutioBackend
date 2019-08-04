using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimPerson
    {
        public long PersonResponsabilityTypeId { get; set; }

        public PersonResponsabilityType PersonResponsabilityType { get; set; }

        public long PersonId { get; set; }

        public Person Person { get; set; }

        public long ClaimId { get; set; }

        public Claim Claim { get; set; }
    }
}
