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

        public int DocumentNumber { get; set; }

        public int TelephoneNumber { get; set; }

        public int MobileNumber { get; set; }

        public string Email { get; set; }

       // public string Adress { get; set; }

        public long PersonTypeId { get; set; }

        public PersonTypeDto PersonType { get; set; }
    }
}
