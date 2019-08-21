using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public long ProvinceId { get; set; }
    }
}
