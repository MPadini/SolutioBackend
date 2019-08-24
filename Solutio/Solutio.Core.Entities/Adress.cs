using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class Adress
    {
        public City City { get; set; }

        public long CityId { get; set; }

        public Province Province { get; set; }

        public long ProvinceId { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Intersection { get; set; }
    }
}
