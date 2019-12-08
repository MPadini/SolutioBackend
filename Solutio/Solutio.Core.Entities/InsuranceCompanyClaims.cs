using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities {
    public class InsuranceCompanyClaims {
        public long Id { get; set; }

        public string CompanyName { get; set; }

        /* public int S11 { get; set; }
         public int S12 { get; set; }
         public int S13 { get; set; }*/
        public int S21 { get; set; }

        public int S22 { get; set; }

        public int S23 { get; set; }

        /* public int S24 { get; set; }
         public int S31 { get; set; }*/
        public int S32 { get; set; }

        public int S33 { get; set; }

        public int S41 { get; set; }

        /* public int S42 { get; set; }*/

        public int S43 { get; set; }

        public int S44 { get; set; }

        /* public int S81 { get; set; }
         public int S82 { get; set; }
         public int S83 { get; set; }
         public int S84 { get; set; }
         public int S100 { get; set; }*/

        public int TOTAL { get; set; }
    }
}
