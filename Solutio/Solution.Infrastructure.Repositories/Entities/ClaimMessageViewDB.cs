using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimMessageViewDB : BaseEntityDB
    {
        public long ClaimId { get; set; }

        public string UserName { get; set; }

        public int UserRoleId { get; set; }

        public string Message { get; set; }

        public bool Viewed { get; set; }

    }
}
