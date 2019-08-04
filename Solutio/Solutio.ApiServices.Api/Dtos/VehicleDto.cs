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

        public long VehicleModelId { get; set; }

        public VehicleModelDto VehicleModel { get; set; }
    }
}
