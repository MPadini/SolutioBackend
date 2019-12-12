using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities {
    public class UserOfficeDB : BaseEntityDB {

        public int UserId { get; set; }

        public long OfficeId { get; set; }
    }
}
