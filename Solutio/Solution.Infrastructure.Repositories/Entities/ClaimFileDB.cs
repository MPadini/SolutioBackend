using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimFileDB : BaseEntityDB
    {
        public long ClaimId { get; set; }

        public string Base64 { get; set; }

        public string FileExtension { get; set; }

        public string FileName { get; set; }

        public long FileTypeId { get; set; }

        public FileTypesDB FileType { get; set; }
    }
}
