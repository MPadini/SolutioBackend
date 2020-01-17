using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos {
    public class ClaimOfferDto {

        public long Id { get; set; }

        public ClaimDto Claim { get; set; }

        public long ClaimId { get; set; }

        public decimal OfferedAmount { get; set; }

        public string PayInstructions { get; set; }

        public int WayToPay { get; set; }

        public long AgreementFileId { get; set; }

        public long SignedAgreementFileId { get; set; }

        public long ClaimOfferStateId { get; set; }

        public ClaimOfferStateDto ClaimOfferState { get; set; }
    }
}
