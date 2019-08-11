using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class PersonDBMap : BaseMap<PersonDB>
    {
        public override void OnConfigure(EntityTypeBuilder<PersonDB> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.DocumentNumber).IsRequired();
            builder.Property(entity => entity.Name).IsRequired();
            builder.Property(entity => entity.Surname).IsRequired();
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
