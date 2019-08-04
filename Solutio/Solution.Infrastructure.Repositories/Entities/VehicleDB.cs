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

        public long VehicleModelId { get; set; }

        public VehicleModelDB VehicleModel { get; set; }
    }
}
