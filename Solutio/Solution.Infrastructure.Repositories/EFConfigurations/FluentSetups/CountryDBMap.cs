using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class CountryDBMap : BaseMap<CountryDB>
    {
        public override void OnConfigure(EntityTypeBuilder<CountryDB> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
