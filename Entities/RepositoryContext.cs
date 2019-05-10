using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Entities
{
    /// <summary>
    /// The <see cref="RepositoryContext"/> class is the main generic <see cref="DbContext"/> 
    /// (AccountOwnerContext) class, that facilitates the <see cref="Owner"/> entity's 
    /// data and it's related data manipulations. It also facilitates the 
    /// <see cref="Account"/> entity's data and it's related data manipulations.
    /// </summary>
    public class RepositoryContext : DbContext
    {
        // Disable Lazy Loading at the context level. It can be enabled 
        // explicitly when it needs to be utilized.
        //public RepositoryContext(DbContextOptions<RepositoryContext> options)
        //    : base(options)
        //{
        //    ChangeTracker.LazyLoadingEnabled = false;
        //}

        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        //public IConfiguration Configuration { get; }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Enable Lazy Loading with a call to method: <see cref="UseLazyLoadingProxies"/>. EF Core will 
        /// then enable lazy loading for any navigation property that can be overridden. Only thing is 
        /// that it must be virtual and on a class that can be inherited from. For example check 
        /// <see cref="OwnerRelated"/> class, the <see cref="Accounts"/> navigation property 
        /// will be lazy-loaded.
        /// </summary>
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //var sqlConnString = Configuration["SQLConnString:DBConnAccountOwner"];

        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        // #warning To protect potentially sensitive information in your connection string, you should move it out of 
        //        // source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //        optionsBuilder
        //            .UseLazyLoadingProxies()
        //            .UseSqlServer(Configuration["SQLConnString:DBConnAccountOwner"]);
        //    }
        //}
    }
}
