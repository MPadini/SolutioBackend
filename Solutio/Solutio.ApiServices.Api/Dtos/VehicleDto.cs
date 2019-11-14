using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class VehicleDto : BaseEntityDto
    {
        public string Patent { get; set; }

        public long VehicleTypeId { get; set; }

        public VehicleTypeDto VehicleType { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleManufacturer { get; set; }

        public string DamageDetail { get; set; }

        public long InsuranceCompanyId { get; set; }

        public InsuranceCompanyDto InsuranceCompany { get; set; }

        public bool HaveFullCoverage { get; set; }

        public decimal? Franchise { get; set; }
    }
}
