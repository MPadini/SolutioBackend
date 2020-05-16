using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities {
    public class ClaimWorkflowDB : BaseEntityDB {

        public long ClaimId { get; set; }

        public long ClaimStateId { get; set; }

        public ClaimStateDB ClaimState { get; set; }

        public string UserName { get; set; }
    }
}
