﻿using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimDocumentServices {
    public interface IGetClaimDocumentService {

        Task<byte[]> GetFile(List<long> claimIds, List<long> documentsIds);
    }
}