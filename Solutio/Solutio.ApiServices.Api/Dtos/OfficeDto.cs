using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos {
    public class OfficeDto : BaseEntityDto {

        public string Name { get; set; }

        public string OwnerName { get; set; }

        public string OwnerDni { get; set; }

        public string OwnerCuit { get; set; }
    }
}
