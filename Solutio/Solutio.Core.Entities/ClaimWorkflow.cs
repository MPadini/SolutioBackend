using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities {
    public class ClaimWorkflow : BaseEntity {

        public static ClaimWorkflow NewInstance() {
            return new ClaimWorkflow();
        }

        public long ClaimId { get; set; }

        public long ClaimStateId { get; set; }

        public ClaimState ClaimState { get; set; }
    }
}
