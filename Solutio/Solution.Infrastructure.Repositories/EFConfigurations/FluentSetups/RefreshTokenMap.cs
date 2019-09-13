using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Core.Entities;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class RefreshTokenMap : BaseMap<RefreshTokenDB>
    {
        public override void OnConfigure(EntityTypeBuilder<RefreshTokenDB> builder)
        {
            builder.HasKey(claim => claim.Id);
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
