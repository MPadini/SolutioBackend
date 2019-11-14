using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities {
    public class InsuranceCompany: BaseEntity {

        public string Name { get; set; }

        public string Adress { get; set; }
    }
}
