using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities {
    public class InsuranceCompanyDB : BaseEntityDB {
        public string Name { get; set; }

        public string Adress { get; set; }
    }
}
