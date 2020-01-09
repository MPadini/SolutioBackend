using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups {
    public class ClaimWorkflowDBMap : BaseMap<ClaimWorkflowDB> {
        public override void OnConfigure(EntityTypeBuilder<ClaimWorkflowDB> builder) {
            builder.HasKey(claim => claim.Id);
            builder.Property(claim => claim.ClaimId).IsRequired();
            builder.Property(claim => claim.ClaimStateId).IsRequired();
        
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
