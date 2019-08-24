using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos.Requests
{
    public class NewClaimRequest
    {
        public string Story { get; set; }

        public DateTime Date { get; set; }

        public string Hour { get; set; }

        public long StateId { get; set; }

        public decimal? TotalBudgetAmount { get; set; }

        public string InsuranceCompany { get; set; }

        public bool HaveFullCoverage { get; set; }

        public decimal? Franchise { get; set; }

        public AdressDto ClaimAdress { get; set; }

        public List<ClaimInsuredPersonSimpleDto> ClaimInsuredPersons { get; set; }

        public List<ClaimInsuredVehicleSimpleDto> ClaimInsuredVehicles { get; set; }
    }

    public class ClaimInsuredVehicleSimpleDto
    {
        public VehicleSimpleDto Vehicle { get; set; }
    }

    public class ClaimInsuredPersonSimpleDto
    {
        public PersonSimpleDto Person { get; set; }
    }

    public class VehicleSimpleDto : BaseEntityDto
    {
        public string Patent { get; set; }

        public long VehicleTypeId { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleManufacturer { get; set; }

        public string DamageDetail { get; set; }
    }

    public class PersonSimpleDto : BaseEntityDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string LegalEntityName { get; set; }

        public string Cuit { get; set; }

        public int DocumentNumber { get; set; }

        public int TelephoneNumber { get; set; }

        public int MobileNumber { get; set; }

        public string Email { get; set; }

        // public string Adress { get; set; }

        public long PersonTypeId { get; set; }
    }
}
