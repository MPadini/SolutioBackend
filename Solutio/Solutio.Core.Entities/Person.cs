using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class Person : BaseEntity
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

        public PersonType PersonType { get; set; }
    }
}
