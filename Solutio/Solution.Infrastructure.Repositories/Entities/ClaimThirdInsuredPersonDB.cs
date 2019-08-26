using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimThirdInsuredPersonDB : BaseEntityDB
    {
        public ClaimThirdInsuredPersonDB()
        {
            Person = new PersonDB();
            Claim = new ClaimDB();
        }

        public static ClaimThirdInsuredPersonDB NewInstance()
        {
            return new ClaimThirdInsuredPersonDB();
        }

        public long PersonId { get; set; }

        public PersonDB Person { get; set; }

        public long ClaimId { get; set; }

        public ClaimDB Claim { get; set; }
    }
}
