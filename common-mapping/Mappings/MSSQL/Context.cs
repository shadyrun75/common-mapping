using common_mapping.Models.MSSQL;
using Microsoft.EntityFrameworkCore;

namespace common_mapping.Mappings.MSSQL
{
    internal class Context : DbContext
    {
        private readonly Settings.MSSQL _setup;

        public Context(Settings.MSSQL setup)
        {
            _setup = setup;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_setup.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(_setup.Schema);
            builder.Entity<MapItem>().ToTable(_setup.TableName_MapItem);
            base.OnModelCreating(builder);            
        }

        public DbSet<MapItem> Items { get; set; } = null!;
    }
}
