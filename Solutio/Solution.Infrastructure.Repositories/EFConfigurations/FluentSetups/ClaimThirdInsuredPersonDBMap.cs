using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class ClaimThirdInsuredPersonDBMap : BaseMap<ClaimThirdInsuredPersonDB>
    {
        public override void OnConfigure(EntityTypeBuilder<ClaimThirdInsuredPersonDB> builder)
        {
            builder.Ignore(entity => entity.Id);
            builder.HasKey(entity => new { entity.PersonId, entity.ClaimId });

            builder.HasOne(entity => entity.Person)
                .WithMany(entity => entity.ClaimThirdPersons)
                .HasForeignKey(entity => entity.PersonId);

            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
