using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities.Base
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
