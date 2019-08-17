using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class ClaimFileDBMap : BaseMap<ClaimFileDB>
    {
        public override void OnConfigure(EntityTypeBuilder<ClaimFileDB> builder)
        {
            builder.HasKey(claim => claim.Id);
            builder.Property(claim => claim.ClaimId).IsRequired();
            builder.Property(claim => claim.Base64).IsRequired();
            builder.Property(claim => claim.FileExtension).IsRequired();
            builder.Property(claim => claim.FileName).IsRequired();
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
