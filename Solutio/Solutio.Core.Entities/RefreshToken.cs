using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string UserName { get; set; }

        public string Refreshtoken { get; set; }
    }
}
