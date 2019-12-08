using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimFile : BaseEntity
    {
        public long ClaimId { get; set; }

        public Claim Claim { get; set; }

        public string Base64 { get; set; }

        public string FileExtension { get; set; }

        public string FileName { get; set; }

        public long FileTypeId { get; set; }

        public FileType FileType { get; set; }

        public bool Printed { get; set; }
    }
}
