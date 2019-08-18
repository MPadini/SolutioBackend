using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimInsuredPerson
    {
        public ClaimInsuredPerson()
        {
            Person = new Person();
        }

        public long PersonId { get; set; }

        public long ClaimId { get; set; }

        public Person Person { get; set; }
    }
}
