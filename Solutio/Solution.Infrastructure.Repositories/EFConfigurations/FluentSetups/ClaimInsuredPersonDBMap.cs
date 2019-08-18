using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class ClaimInsuredPersonDBMap : BaseMap<ClaimInsuredPersonDB>
    {
        public override void OnConfigure(EntityTypeBuilder<ClaimInsuredPersonDB> builder)
        {
            builder.Ignore(entity => entity.Id);
            builder.HasKey(entity => new { entity.PersonId, entity.ClaimId });

            builder.HasOne(entity => entity.Person)
                .WithMany(entity => entity.ClaimPersons)
                .HasForeignKey(entity => entity.PersonId);

            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
