using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups {
    public class ClaimOfferDBMap : BaseMap<ClaimOfferDB> {
        public override void OnConfigure(EntityTypeBuilder<ClaimOfferDB> builder) {
            builder.HasKey(claim => claim.Id);
            builder.Property(claim => claim.ClaimId).IsRequired();
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
