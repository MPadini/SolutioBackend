using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups {
    public class OfficeDBMap : BaseMap<OfficeDB> {
        public override void OnConfigure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OfficeDB> builder) {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
