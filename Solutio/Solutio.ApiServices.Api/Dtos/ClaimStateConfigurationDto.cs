using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class ClaimStateConfigurationDto
    {
        public ClaimStateDto ParentClaimState { get; set; }

        public List<ClaimStateDto> ChildrenClaimState { get; set; }
    }
}
