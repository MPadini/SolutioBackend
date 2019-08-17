using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class Claim : BaseEntity
    {
        public string Story { get; set; }

        public DateTime Date { get; set; }

        public DateTime Hour { get; set; }

        public long StateId { get; set; }

        public ClaimState State { get; set; }

        //public List<ClaimPerson> ClaimPersons { get; set; }

        //public List<ClaimVehicle> ClaimVehicles { get; set; }

        public long InsuredPersonId { get; set; }

        public Person InsuredPerson { get; set; }

        public long InsuredVehicleId { get; set; }

        public Vehicle InsuredVehicle { get; set; }
    }
}
