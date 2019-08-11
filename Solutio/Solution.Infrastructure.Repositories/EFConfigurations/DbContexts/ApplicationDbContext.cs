using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Remotion.Linq.Parsing.ExpressionVisitors;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Solutio.Infrastructure.Repositories.Entities;
using Microsoft.AspNetCore.Identity;
using Solutio.Infrastructure.Repositories.EFConfigurations.FluentSetups;

namespace Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        #region constants

        private readonly IConfiguration configuration;

        #endregion constants

        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(IConfiguration configuration)
           : base()
        {
            this.configuration = configuration;
        }

        public ApplicationDbContext(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.configuration = configuration;
        }

        #endregion Constructor

        #region DbSet Setups

        public DbSet<ClaimDB> Claims { get; set; }

        public DbSet<ClaimStateDB> ClaimStates { get; set; }

        public DbSet<ClaimPersonDB> ClaimPersons { get; set; }

        public DbSet<PersonDB> Persons { get; set; }

        public DbSet<PersonTypeDB> PersonTypes { get; set; }

        public DbSet<PersonResponsabilityTypeDB> PersonResponsabilityTypes { get; set; }

        public DbSet<VehicleParticipationTypeDB> VehicleParticipationTypes { get; set; }

        public DbSet<VehicleModelDB> VehicleModels { get; set; }

        public DbSet<VehicleTypeDB> VehicleTypes { get; set; }

        public DbSet<VehicleDB> Vehicles { get; set; }

        public DbSet<ClaimVehicleDB> ClaimVehicles { get; set; }

        #endregion DbSet Setups

        public override int SaveChanges()
        {
            SetLogicalDelete();
            SetUpdateDate();
            
            return base.SaveChanges();
        }

        private void SetLogicalDelete()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted &&
             e.Metadata.GetProperties().Any(x => x.Name == "Deleted")))
            {
                item.State = EntityState.Unchanged;
                item.CurrentValues["Deleted"] = DateTime.Now;
            }
        }

        private void SetUpdateDate()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified &&
                         e.Metadata.GetProperties().Any(x => x.Name == "Modified")))
            {
                item.CurrentValues["Modified"] = DateTime.Now;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ClaimDBMap());
            modelBuilder.ApplyConfiguration(new ClaimPersonDBMap());
            modelBuilder.ApplyConfiguration(new ClaimStateDBMap());
            modelBuilder.ApplyConfiguration(new ClaimVehicleDBMap());
            modelBuilder.ApplyConfiguration(new PersonDBMap());
            modelBuilder.ApplyConfiguration(new PersonResponsabilityTypeDBMap());
            modelBuilder.ApplyConfiguration(new PersonTypeDBMap());
            modelBuilder.ApplyConfiguration(new VehicleDBMap());
            modelBuilder.ApplyConfiguration(new VehicleModelDBMap());
            modelBuilder.ApplyConfiguration(new VehicleTypeDBMap());
            modelBuilder.ApplyConfiguration(new VehicleParticipationTypeDBMap());
        }
    }
}