using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.FileService;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.FileService {
    public class UpdateFileService : IUpdateFileService {
        private readonly IClaimFileRepository claimFileRepository;

        public UpdateFileService(IClaimFileRepository claimFileRepository) {
            this.claimFileRepository = claimFileRepository;
        }

        public async Task MarkAsPrinted(List<ClaimFile> claimFiles) {
            if (claimFiles == null) return;
            if (!claimFiles.Any()) return;

            List<ClaimFile> files = new List<ClaimFile>();

            try {
                foreach (var file in claimFiles) {
                   // ClaimFile updatedFile = new ClaimFile();
                    file.Printed = true;
                  //  files.Add(updatedFile);
                }

                await claimFileRepository.Update(claimFiles);
            }
            catch (Exception) {
                //log
            }
           
        }
    }
}
