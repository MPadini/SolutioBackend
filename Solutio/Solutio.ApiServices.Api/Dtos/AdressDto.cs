using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class AdressDto
    {
        public CityDto City { get; set; }

        public ProvinceDto Province { get; set; }

        public long CityId { get; set; }

        public long ProvinceId { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Intersection { get; set; }

        public string Lat { get; set; }

        public string Lng { get; set; }
    }
}
