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
            ClaimInsuredPersons = new List<ClaimInsuredPerson>();
            ClaimInsuredVehicles = new List<ClaimInsuredVehicle>();
        }

        public string Story { get; set; }

        public DateTime Date { get; set; }

        public DateTime Hour { get; set; }

        public long StateId { get; set; }

        public ClaimState State { get; set; }

        public List<ClaimInsuredPerson> ClaimInsuredPersons { get; set; }

        public List<ClaimInsuredVehicle> ClaimInsuredVehicles { get; set; }

        public decimal? TotalBudgetAmount { get; set; }

        public string InsuranceCompany { get; set; }

        public bool HaveFullCoverage { get; set; }

        public decimal? Franchise { get; set; }
    }
}
