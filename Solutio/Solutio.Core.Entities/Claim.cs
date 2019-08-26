using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class Claim : BaseEntity
    {
        public Claim()
        {
            Files = new List<ClaimFile>();
        }

        public string Story { get; set; }

        public DateTime Date { get; set; }

        public string Hour { get; set; }

        public long StateId { get; set; }

        public ClaimState State { get; set; }

        public List<Person> ClaimInsuredPersons { get; set; }

        public List<Vehicle> ClaimInsuredVehicles { get; set; }

        public List<Person> ClaimThirdInsuredPersons { get; set; }

        public List<Vehicle> ClaimThirdInsuredVehicles { get; set; }

        public decimal? TotalBudgetAmount { get; set; }

        public List<ClaimFile> Files { get; set; }

        public Adress Adress { get; set; }
    }
}
