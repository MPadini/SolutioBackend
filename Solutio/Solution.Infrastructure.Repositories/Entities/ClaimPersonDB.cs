using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimPersonDB
    {
        public long PersonResponsabilityTypeId { get; set; }

        public PersonResponsabilityTypeDB PersonResponsabilityType { get; set; }

        public long PersonId { get; set; }

        public PersonDB Person { get; set; }

        public long ClaimId { get; set; }

        public ClaimDB Claim { get; set; }
    }
}
