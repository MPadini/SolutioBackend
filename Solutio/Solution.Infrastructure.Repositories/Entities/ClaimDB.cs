using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimDB : BaseEntityDB
    {
        public string Story { get; set; }

        public DateTime Date { get; set; }

        public DateTime Hour { get; set; }

        public long StateId { get; set; }

        public ClaimStateDB State { get; set; }

        public List<ClaimPersonDB> ClaimPersons { get; set; }

        public List<ClaimVehicleDB> ClaimVehicles { get; set; }
    }
}
