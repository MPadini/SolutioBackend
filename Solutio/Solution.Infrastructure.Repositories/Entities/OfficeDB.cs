using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities {
    public class OfficeDB : BaseEntityDB {

        public string Name { get; set; }

        public string OwnerName { get; set; }

        public string OwnerDni { get; set; }

        public string OwnerCuit { get; set; }
    }
}
