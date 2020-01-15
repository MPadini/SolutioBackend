using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class PersonDto : BaseEntityDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string LegalEntityName { get; set; }

        public string Cuit { get; set; }

        public string DocumentNumber { get; set; }

        public string TelephoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        // public string Adress { get; set; }

        public bool WasInjured { get; set; }

        public bool IsCarHolder { get; set; }

        public long PersonTypeId { get; set; }

        public PersonTypeDto PersonType { get; set; }
    }
}
