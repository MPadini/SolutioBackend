using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class ClaimStateConfigurationDBMap : BaseMap<ClaimStateConfigurationDB>
    {
        public override void OnConfigure(EntityTypeBuilder<ClaimStateConfigurationDB> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.Deleted == null);

            builder.HasOne(entity => entity.ParentClaimState)
               .WithMany(entity => entity.StateConfigurations)
               .HasForeignKey(entity => entity.ParentClaimStateId);
        }
    }
}
