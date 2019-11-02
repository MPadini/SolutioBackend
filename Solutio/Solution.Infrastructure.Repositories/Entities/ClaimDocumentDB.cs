using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities {
    public class ClaimDocumentDB : BaseEntityDB {

        public long Id { get; set; }

        public string HtmlTemplate { get; set; }
    }
}
