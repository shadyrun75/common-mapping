using common_mapping.Models.SQLite;
using Microsoft.EntityFrameworkCore;

namespace common_mapping.Mappings.SQLite
{
    internal class Context : DbContext
    {
        private readonly Settings.SQLite _setup;

        public Context(Settings.SQLite setup)
        {
            _setup = setup;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source={_setup.DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");
            base.OnModelCreating(builder);            
        }
        
        public DbSet<MapItem> Items { get; set; } = null!;
        public DbSet<MapType> Types { get; set; } = null!;
        public DbSet<MapLink> Links { get; set; } = null!;
    }
}
