using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class Vehicle : BaseEntity
    {
        public string Patent { get; set; }

        public long VehicleTypeId { get; set; }

        public VehicleType VehicleType { get; set; }

        public long VehicleModelId { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleManufacturer { get; set; }

        public string DamageDetail { get; set; }
    }
}
