using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups {
    public class ClaimOfferStateDBMap : BaseMap<ClaimOfferStateDB> {
        public override void OnConfigure(EntityTypeBuilder<ClaimOfferStateDB> builder) {
            builder.HasKey(claim => claim.Id);
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
