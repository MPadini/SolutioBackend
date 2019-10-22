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
using System.Threading.Tasks;

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

        public DbSet<ClaimStateConfigurationDB> ClaimStateConfigurations { get; set; }

        public DbSet<ClaimInsuredPersonDB> ClaimInsuredPersons { get; set; }

        public DbSet<ClaimThirdInsuredPersonDB> ClaimThirdInsuredPersons { get; set; }

        public DbSet<PersonDB> Persons { get; set; }

        public DbSet<PersonTypeDB> PersonTypes { get; set; }

        public DbSet<VehicleTypeDB> VehicleTypes { get; set; }

        public DbSet<VehicleDB> Vehicles { get; set; }

        public DbSet<ClaimInsuredVehicleDB> ClaimInsuredVehicles { get; set; }

        public DbSet<ClaimThirdInsuredVehicleDB> ClaimThirdInsuredVehicles { get; set; }

        public DbSet<ClaimFileDB> ClaimFiles { get; set; }

        public DbSet<ProvinceDB> Provinces { get; set; }

        public DbSet<CityDB> Cities { get; set; }

        public DbSet<CountryDB> Countries { get; set; }

        public DbSet<AdressDB> Adresses { get; set; }

        public DbSet<RefreshTokenDB> RefreshTokens { get; set; }

        public DbSet<ClaimDocumentDB> ClaimDocuments { get; set; }

        #endregion DbSet Setups

        public override int SaveChanges()
        {
            SetCreateDate();
            SetUpdateDate();
            SetLogicalDelete();
                   
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

        private void SetCreateDate()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added &&
             e.Metadata.GetProperties().Any(x => x.Name == "Created")))
            {
                item.CurrentValues["Created"] = DateTime.Now;
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
            modelBuilder.ApplyConfiguration(new ClaimInsuredPersonDBMap());
            modelBuilder.ApplyConfiguration(new ClaimStateDBMap());
            modelBuilder.ApplyConfiguration(new ClaimInsuredVehicleDBMap());
            modelBuilder.ApplyConfiguration(new PersonDBMap());
            modelBuilder.ApplyConfiguration(new PersonTypeDBMap());
            modelBuilder.ApplyConfiguration(new VehicleDBMap());
            modelBuilder.ApplyConfiguration(new VehicleTypeDBMap());
            modelBuilder.ApplyConfiguration(new ClaimFileDBMap());
            modelBuilder.ApplyConfiguration(new ProvinceDBMap());
            modelBuilder.ApplyConfiguration(new CityDBMap());
            modelBuilder.ApplyConfiguration(new CountryDBMap());
            modelBuilder.ApplyConfiguration(new AdressDBMap());
            modelBuilder.ApplyConfiguration(new ClaimThirdInsuredPersonDBMap());
            modelBuilder.ApplyConfiguration(new ClaimThirdInsuredVehicleDBMap());
            modelBuilder.ApplyConfiguration(new ClaimStateConfigurationDBMap());
            modelBuilder.ApplyConfiguration(new RefreshTokenMap());
        }
    }
}