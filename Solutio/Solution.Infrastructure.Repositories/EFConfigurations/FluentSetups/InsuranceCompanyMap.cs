using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups {

    public class InsuranceCompanyMap : BaseMap<InsuranceCompanyDB> {
        public override void OnConfigure(EntityTypeBuilder<InsuranceCompanyDB> builder) {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
