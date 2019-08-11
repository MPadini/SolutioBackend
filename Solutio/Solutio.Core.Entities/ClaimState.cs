using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimState : BaseEntity
    {
        public string Description { get; set; }

        public bool CanInDraft { get; set; }

        public bool CanAudit { get; set; }

        public bool CanPresented { get; set; }

        public bool CanWaitForAction { get; set; }

        public bool CanInMonitoring { get; set; }

        public bool CanOffered { get; set; }

        public bool CanAcepted { get; set; }

        public bool CanOutstanding { get; set; }

        public bool CanClose { get; set; }

        public int MaximumTimeAllowed { get; set; }
    }
}
