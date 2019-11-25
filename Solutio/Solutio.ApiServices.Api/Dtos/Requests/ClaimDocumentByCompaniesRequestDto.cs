using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos.Requests {
    public class ClaimDocumentByCompaniesRequestDto {

        public long Id { get; set; }
        public string CompanyName { get; set; }
    }
}
