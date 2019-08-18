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

        public int DocumentNumber { get; set; }

        public int TelephoneNumber { get; set; }

        public int MobileNumber { get; set; }

        public string Email { get; set; }

        //public string Adress { get; set; }

        public long PersonTypeId { get; set; }

        public PersonTypeDB PersonType { get; set; }

        public List<ClaimInsuredPersonDB> ClaimPersons { get; set; }
    }
}
