using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class ClaimInsuredVehicleDBMap : BaseMap<ClaimInsuredVehicleDB>
    {
        public override void OnConfigure(EntityTypeBuilder<ClaimInsuredVehicleDB> builder)
        {
            builder.Ignore(entity => entity.Id);
            builder.HasKey(entity => new { entity.VehicleId, entity.ClaimId });

            builder.HasOne(entity => entity.Vehicle)
                .WithMany(entity => entity.ClaimVehicles)
                .HasForeignKey(entity => entity.VehicleId);

            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
