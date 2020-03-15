using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities {
    public class Office : BaseEntity {

        public string Name { get; set; }
        public string OwnerName { get; set; }

        public string OwnerDni { get; set; }

        public string OwnerCuit { get; set; }

    }
}
