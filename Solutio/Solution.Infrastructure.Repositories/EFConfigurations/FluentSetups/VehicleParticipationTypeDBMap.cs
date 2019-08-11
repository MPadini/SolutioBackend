using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    class VehicleParticipationTypeDBMap : BaseMap<VehicleParticipationTypeDB>
    {
        public override void OnConfigure(EntityTypeBuilder<VehicleParticipationTypeDB> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
