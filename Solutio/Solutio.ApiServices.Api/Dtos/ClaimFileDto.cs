using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class ClaimFileDto
    {
        public long ClaimId { get; set; }

        public string Base64 { get; set; }

        public string FileExtension { get; set; }

        public string FileName { get; set; }
    }
}
