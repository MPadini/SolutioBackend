using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimMessage : BaseEntity
    {
        public long ClaimId { get; set; }

        public string UserName{ get; set; }

        public string Message { get; set; }

        public bool Viewed { get; set; }
    }
}
