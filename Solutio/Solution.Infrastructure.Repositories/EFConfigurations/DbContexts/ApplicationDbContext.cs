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

namespace Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int> //IdentityDbContext<ApplicationUser<int>, int>
    {
        #region constants

        private readonly IConfiguration configuration;
        private readonly string connectionStringName = "SolutioConnectionString";

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

        // public DbSet<PracticalWork> PracticalWorks { get; set; }


        #endregion DbSet Setups

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new StageMap());
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (configuration != null)
        //    {
        //        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        //    }
        //}
    }
}