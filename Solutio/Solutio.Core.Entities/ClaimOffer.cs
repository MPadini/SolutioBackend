using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities {
    public class ClaimOffer : BaseEntity {

        public Claim Claim { get; set; }

        public long ClaimId { get; set; }

        public decimal OfferedAmount { get; set; }

        public string PayInstructions { get; set; }

        public int WayToPay { get; set; }

        public long AgreementFileId { get; set; }

        public long SignedAgreementFileId { get; set; }

        public long ClaimOfferStateId { get; set; }

        public ClaimOfferState ClaimOfferState { get; set; }
    }
}
