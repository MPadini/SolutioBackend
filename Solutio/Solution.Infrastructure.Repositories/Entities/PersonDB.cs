using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class PersonDB : BaseEntityDB
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string LegalEntityName { get; set; }

        public string Cuit { get; set; }

        public string DocumentNumber { get; set; }

        public string TelephoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        //public string Adress { get; set; }

        public long PersonTypeId { get; set; }

        public PersonTypeDB PersonType { get; set; }

        public List<ClaimInsuredPersonDB> ClaimPersons { get; set; }

        public List<ClaimThirdInsuredPersonDB> ClaimThirdPersons { get; set; }
    }
}
