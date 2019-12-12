using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities {
    public class UserOffice {
        public int UserId { get; set; }

        public long OfficeId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
