using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class VehicleDBMap : BaseMap<VehicleDB>
    {
        public override void OnConfigure(EntityTypeBuilder<VehicleDB> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Patent).IsRequired();
        }
    }
}
