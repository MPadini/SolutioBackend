﻿using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Solutio.Infrastructure.Repositories.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Solutio.Infrastructure.Repositories.Mappers;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimFileRepository : IClaimFileRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IClaimMapper claimMapper;

        public ClaimFileRepository(ApplicationDbContext applicationDbContext, IClaimMapper claimMapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.claimMapper = claimMapper;
        }

        public async Task<long> Upload(ClaimFile file)
        {
            try
            {
                var fileDb = file.Adapt<ClaimFileDB>();

                applicationDbContext.Add(fileDb);
                applicationDbContext.SaveChanges();

                return fileDb.Id;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task Delete(ClaimFile file)
        {
            var fileDb = file.Adapt<ClaimFileDB>();

            applicationDbContext.Remove(fileDb);
            applicationDbContext.SaveChanges();
        }

        public async Task<ClaimFile> GetById(long fileId, bool withBase64)
        {
            if (withBase64) {
                var file = applicationDbContext.ClaimFiles
               .AsNoTracking().Include(x => x.FileType)
               .FirstOrDefault(x => x.Id == fileId);

                return file.Adapt<ClaimFile>();
            }
            else {
                var file = applicationDbContext.ClaimFiles
                .AsNoTracking().Include(x => x.FileType)
                .Where(x => x.Id == fileId).Select(x => new {
                    x.Id,
                    x.ClaimId,
                    x.FileName,
                    x.FileType,
                    x.FileTypeId
                }).FirstOrDefault();

                return file.Adapt<ClaimFile>();
            }
            
        }

        public async Task<List<ClaimFile>> GetByClaimId(long claimId, bool withBase64) {
            if (withBase64) {
                var filesWithBase64 = applicationDbContext.ClaimFiles
              .AsNoTracking().Include(x => x.FileType).Where(x => x.ClaimId == claimId).Select(x => new {
                  x.Id,
                  x.ClaimId,
                  x.Base64,
                  x.FileName,
                  x.FileType,
                  x.FileTypeId,
                  x.Printed
              }).ToList();

                return filesWithBase64.Adapt<List<ClaimFile>>();

            } 
            var files = applicationDbContext.ClaimFiles
                .AsNoTracking().Include(x => x.FileType).Where(x => x.ClaimId == claimId).Select(x => new {
                    x.Id,
                    x.ClaimId,
                    x.FileName,
                    x.FileType,
                    x.FileTypeId,
                    x.Printed
                }).ToList();

            return files.Adapt<List<ClaimFile>>();
        }

        public async Task DeleteClaimFiles(Claim claim)
        {
            var claimDB = claimMapper.Map(claim);
            if (claimDB.Files != null)
            {
                claimDB.Files.ForEach(file =>
                {
                    applicationDbContext.ClaimFiles.Remove(file);
                    applicationDbContext.SaveChanges();
                });
            }
        }

        public async Task<List<FileType>> GetFileTypes() {
            var result = applicationDbContext.FileTypes.AsNoTracking().ToList();
            return result.Adapt<List<FileType>>();
        }

        public async Task Update(List<ClaimFile> claimFiles) {
            if (claimFiles == null) return;

            var claimFilesDB = claimFiles.Adapt<List<ClaimFileDB>>();

            foreach (var file in claimFiles) {
                applicationDbContext.Database.ExecuteSqlCommand("update ClaimFiles set printed = 1 where id = {0}", file.Id);
            }
           
        }
    }
}
