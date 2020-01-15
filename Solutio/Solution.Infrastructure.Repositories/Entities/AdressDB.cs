using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class AdressDB : BaseEntityDB
    {
        public CityDB City { get; set; }

        public long CityId { get; set; }

        public ProvinceDB Province { get; set; }

        public long ProvinceId { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Intersection { get; set; }

        public string BetweenStreets { get; set; }

        public string Lat { get; set; }

        public string Lng { get; set; }
    }
}
