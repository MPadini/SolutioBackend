using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class ClaimMessageDto
    {
        public long ClaimId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        public bool Viewed { get; set; }
    }
}
