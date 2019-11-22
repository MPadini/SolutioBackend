using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos.Requests {
    public class ClaimDocumentRequestDto {

        public List<long> ClaimIds { get; set; }

        public List<long> DocumentIds { get; set; }

        public List<long> ClaimFiles { get; set; }
    }
}
