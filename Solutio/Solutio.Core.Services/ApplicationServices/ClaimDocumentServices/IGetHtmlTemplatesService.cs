﻿using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimDocumentServices {
    public interface IGetHtmlTemplatesService {

        Task<List<ClaimDocument>> GetHtmlTemplates();
    }
}
