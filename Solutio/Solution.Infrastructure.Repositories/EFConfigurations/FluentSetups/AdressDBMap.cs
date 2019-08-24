using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class AdressDBMap : BaseMap<AdressDB>
    {
        public override void OnConfigure(EntityTypeBuilder<AdressDB> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
