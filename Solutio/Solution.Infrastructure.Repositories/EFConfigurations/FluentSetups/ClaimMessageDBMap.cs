using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class ClaimMessageDBMap : BaseMap<ClaimMessageDB>
    {
        public override void OnConfigure(EntityTypeBuilder<ClaimMessageDB> builder)
        {
            builder.HasKey(message => message.Id);
            builder.Property(message => message.ClaimId).IsRequired();
            builder.Property(message => message.UserName).IsRequired();
            builder.Property(message => message.Message).IsRequired();
            builder.Property(message => message.Viewed).IsRequired();
            builder.HasQueryFilter(message => message.Deleted == null);
        }
    }
}
