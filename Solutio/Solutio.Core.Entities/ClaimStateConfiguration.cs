using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimStateConfiguration : BaseEntity
    {
        public ClaimStateConfiguration()
        {
            ChildrenClaimState = new ClaimState();
        }

        public static ClaimStateConfiguration NewInstance()
        {
            return new ClaimStateConfiguration();
        }

        public ClaimState ChildrenClaimState { get; set; }
    }
}
