using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups {
    public class UserOfficeDBMap : BaseMap<UserOfficeDB> {

        public override void OnConfigure(EntityTypeBuilder<UserOfficeDB> builder) {
            builder.Ignore(entity => entity.Id);
            builder.HasKey(entity => new { entity.UserId, entity.OfficeId });
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
