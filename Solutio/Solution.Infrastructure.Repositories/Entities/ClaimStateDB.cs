using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimStateDB : BaseEntityDB
    {
        public string Description { get; set; }

        public int MaximumTimeAllowed { get; set; }

        public List<ClaimStateConfigurationDB> StateConfigurations { get; set; }
    }
}
