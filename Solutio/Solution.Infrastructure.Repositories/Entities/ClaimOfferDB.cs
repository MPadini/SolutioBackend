using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities {
    public class ClaimOfferDB : BaseEntityDB {

        public long ClaimId { get; set; }

        public decimal OfferedAmount { get; set; }

        public string PayInstructions { get; set; }

        public int WayToPay { get; set; }

        public long AgreementFileId { get; set; }

        public long SignedAgreementFileId { get; set; }

        public long ClaimOfferStateId { get; set; }

        public ClaimOfferStateDB ClaimOfferState { get; set; }
    }
}
