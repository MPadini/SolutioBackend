using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimState : BaseEntity
    {
        public ClaimState()
        {
            AllowedStates = new List<ClaimState>();
        }

        public string Description { get; set; }

        public int MaximumTimeAllowed { get; set; }

        public List<ClaimState> AllowedStates { get; set; }

        public enum eId:long
        {
            InDraft = 1,
            Audit = 2,
            Presented = 3,
            WaitForAction = 4,
            InMonitoring = 5,
            Offered = 6,
            Acepted = 7,
            Outstanding = 8,
            Close = 9
        }
    }
}
