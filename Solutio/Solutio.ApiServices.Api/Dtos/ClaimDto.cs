﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class ClaimDto : BaseEntityDto
    {
        public string Story { get; set; }

        public DateTime Date { get; set; }

        public DateTime Hour { get; set; }

        public long StateId { get; set; }

        public List<ClaimInsuredPersonDto> ClaimInsuredPersons { get; set; }

        public List<ClaimInsuredVehicleDto> ClaimInsuredVehicles { get; set; }

        public decimal TotalBudgetAmount { get; set; }

        public string InsuranceCompany { get; set; }

        public bool HaveFullCoverage { get; set; }

        public decimal Franchise { get; set; }
    }
}
