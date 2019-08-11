using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class VehicleModelDBMap : BaseMap<VehicleModelDB>
    {
        public override void OnConfigure(EntityTypeBuilder<VehicleModelDB> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
