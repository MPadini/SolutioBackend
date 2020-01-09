using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos {
    public class ClaimWorkflowDto {

        public long Id { get; set; }

        public DateTime Created { get; set; }

        public long ClaimId { get; set; }

        public ClaimStateDto ClaimState { get; set; }
    }
}
