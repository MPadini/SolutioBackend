using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimStateConfigurationDB : BaseEntityDB
    {
        public long ParentClaimStateId { get; set; }

        public ClaimStateDB ParentClaimState { get; set; }

        public long AllowedStateId { get; set; }

        public ClaimStateDB AllowedState { get; set; }
    }
}
