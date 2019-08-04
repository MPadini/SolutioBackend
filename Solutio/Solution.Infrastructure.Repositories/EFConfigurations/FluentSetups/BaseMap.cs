using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups
{
    public abstract class BaseMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntityDB
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            OnConfigure(builder);
        }

        public abstract void OnConfigure(EntityTypeBuilder<TEntity> builder);
    }
}
