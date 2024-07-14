using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VAN.SQLServerCore.SQLServer.Models;

namespace VAN.SQLServerCore.SQLServer
{
    public class SQLServerInit : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public string ConnectionString { get; init; } = default!;

        public SQLServerInit(string connectionString) {
            ConnectionString = connectionString;  
        }

        public SQLServerInit(DbContextOptions<SQLServerInit> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user", "work")
                .HasKey(u => u.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

    }
}
