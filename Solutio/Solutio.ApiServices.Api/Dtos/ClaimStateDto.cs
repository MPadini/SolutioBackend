using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class ClaimStateDto : BaseEntityDto
    {
        public string Description { get; set; }

        public int MaximumTimeAllowed { get; set; }

        public List<ClaimStateDto> AllowedStates { get; set; }
    }
}
