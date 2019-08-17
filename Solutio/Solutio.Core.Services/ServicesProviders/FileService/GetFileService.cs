﻿using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.FileService;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.FileService
{
    public class GetFileService : IGetFileService
    {
        private readonly IClaimFileRepository claimFileRepository;

        public GetFileService(IClaimFileRepository claimFileRepository)
        {
            this.claimFileRepository = claimFileRepository;
        }

        public async Task<ClaimFile> GetById(long id)
        {
            return await claimFileRepository.GetById(id);
        }
    }
}
