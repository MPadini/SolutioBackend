using System;
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

        public string Hour { get; set; }

        public long StateId { get; set; }

        public DateTime StateModifiedDate { get; set; }

        public ClaimStateDB State { get; set; }

        public List<ClaimInsuredPersonDB> ClaimInsuredPersons { get; set; }

        public List<ClaimInsuredVehicleDB> ClaimInsuredVehicles { get; set; }

        public List<ClaimThirdInsuredPersonDB> ClaimThirdInsuredPersons { get; set; }

        public List<ClaimThirdInsuredVehicleDB> ClaimThirdInsuredVehicles { get; set; }

        public decimal? TotalBudgetAmount { get; set; }

        public List<ClaimFileDB> Files { get; set; }

        public long? AdressId { get; set; }

        public AdressDB Adress { get; set; }

        public string UserName { get; set; }

        public bool Printed { get; set; }
    }
}
