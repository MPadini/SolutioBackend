using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimDocumentServices {
    public interface IHtmlToPdfHelperService {

        Task<byte[]> GetFile(string htmlString);
    }
}
