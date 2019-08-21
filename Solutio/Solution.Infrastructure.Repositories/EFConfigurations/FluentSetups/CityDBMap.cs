using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class CityDBMap : BaseMap<CityDB>
    {
        public override void OnConfigure(EntityTypeBuilder<CityDB> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
