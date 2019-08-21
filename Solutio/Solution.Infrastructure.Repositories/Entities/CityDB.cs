using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class CityDB : BaseEntityDB
    {
        public string Name { get; set; }

        public long ProvinceId { get; set; }

        public ProvinceDB Province { get; set; }
    }
}
