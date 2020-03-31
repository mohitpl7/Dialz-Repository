using Dialz.Foundation.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dialz.Foundation.EntityFramework.Providers.DataAccessPostgreSqlProvider
{
    // >dotnet ef migration add testMigration in AspNet5MultipleProject
    public class DomainModelPostgreSqlContext : DbContext
    {
        public DomainModelPostgreSqlContext(DbContextOptions<DomainModelPostgreSqlContext> options) : base(options)
        {
        }

        public DbSet<DataEventRecord> DataEventRecords { get; set; }

        public DbSet<SourceInfo> SourceInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
            builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);

            // shadow properties
            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<SourceInfo>();
            updateUpdatedProperty<DataEventRecord>();

            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Use a PostgreSQL database
            var sqlConnectionString = Microsoft.Extensions.Configuration.GetConnectionString("DataAccessPostgreSqlProvider");

            services.AddDbContext<DomainModelPostgreSqlContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly("AspNet5MultipleProject")
                )
            );

            services.AddScoped<IDataAccessProvider, DataAccessPostgreSqlProvider.DataAccessPostgreSqlProvider>();

            JsonOutputFormatter jsonOutputFormatter = new JsonOutputFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            };

            services.AddMvc(
                options =>
                {
                    options.OutputFormatters.Clear();
                    options.OutputFormatters.Insert(0, jsonOutputFormatter);
                }
            );
        }
    }
}
