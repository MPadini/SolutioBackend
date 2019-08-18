﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimDB : BaseEntityDB
    {
        public ClaimDB()
        {
            ClaimInsuredPersons = new List<ClaimInsuredPersonDB>();
            ClaimInsuredVehicles = new List<ClaimInsuredVehicleDB>();
            Files = new List<ClaimFileDB>();
        }

        public string Story { get; set; }

        public DateTime Date { get; set; }

        public DateTime Hour { get; set; }

        public long StateId { get; set; }

        public ClaimStateDB State { get; set; }

        public List<ClaimInsuredPersonDB> ClaimInsuredPersons { get; set; }

        public List<ClaimInsuredVehicleDB> ClaimInsuredVehicles { get; set; }

        public decimal? TotalBudgetAmount { get; set; }

        public string InsuranceCompany { get; set; }

        public bool HaveFullCoverage { get; set; }

        public decimal? Franchise { get; set; }

        public List<ClaimFileDB> Files { get; set; }
    }
}
