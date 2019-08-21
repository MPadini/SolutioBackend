using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ProvinceDB : BaseEntityDB
    {
        public string Name { get; set; }

        public long CountryId { get; set; }

        public CountryDB Country { get; set; }
    }
}
