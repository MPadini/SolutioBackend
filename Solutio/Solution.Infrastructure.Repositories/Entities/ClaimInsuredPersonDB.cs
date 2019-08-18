using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimInsuredPersonDB : BaseEntityDB
    {
        public ClaimInsuredPersonDB()
        {
            Person = new PersonDB();
            Claim = new ClaimDB();
        }

        public long PersonId { get; set; }

        public PersonDB Person { get; set; }

        public long ClaimId { get; set; }

        public ClaimDB Claim { get; set; }
    }
}
