using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos {
    public class InsuranceCompanyDto : BaseEntityDto {

        public string Name { get; set; }

        public string Adress { get; set; }
    }
}
