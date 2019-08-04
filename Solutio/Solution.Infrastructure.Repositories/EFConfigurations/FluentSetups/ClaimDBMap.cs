﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public class ClaimDBMap : BaseMap<ClaimDB>
    {
        public override void OnConfigure(EntityTypeBuilder<ClaimDB> builder)
        {
            builder.HasKey(claim => claim.Id);
            builder.Property(claim => claim.Date).IsRequired();
            builder.Property(claim => claim.Hour).IsRequired();
        }
    }
}
