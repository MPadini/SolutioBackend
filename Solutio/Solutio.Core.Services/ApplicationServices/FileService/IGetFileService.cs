using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.FileService
{
    public interface IGetFileService
    {
        Task<ClaimFile> GetById(long id);

        Task<List<FileType>> GetFileTypes();
    }
}
