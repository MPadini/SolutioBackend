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

        public int DocumentNumber { get; set; }

        public int TelephoneNumber { get; set; }

        public int MobileNumber { get; set; }

        public string Email { get; set; }

        public string Adress { get; set; }

        public long PersonTypeId { get; set; }

        public PersonType PersonType { get; set; }
    }
}
