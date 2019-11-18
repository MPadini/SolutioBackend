using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class VehicleDB : BaseEntityDB
    {
        public string Patent { get; set; }

        public long VehicleTypeId { get; set; }

        public VehicleTypeDB VehicleType { get; set; }

        public List<ClaimInsuredVehicleDB> ClaimVehicles { get; set; }

        public List<ClaimThirdInsuredVehicleDB> ClaimThirdVehicles { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleManufacturer { get; set; }

        public string DamageDetail { get; set; }

        public long? InsuranceCompanyId { get; set; }

        public InsuranceCompanyDB InsuranceCompany { get; set; }

        public bool HaveFullCoverage { get; set; }

        public decimal? Franchise { get; set; }
    }
}
