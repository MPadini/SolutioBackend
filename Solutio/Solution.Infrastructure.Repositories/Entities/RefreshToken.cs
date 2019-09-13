using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class RefreshTokenDB : BaseEntityDB
    {
        public string UserName { get; set; }

        public string Refreshtoken { get; set; }
    }
}
