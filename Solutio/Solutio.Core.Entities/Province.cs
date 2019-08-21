using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class Province : BaseEntity
    {
        public string Name { get; set; }

        public long CountryId { get; set; }
    }
}
