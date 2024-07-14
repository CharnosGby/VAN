using Microsoft.EntityFrameworkCore;
using VAN.SQLServerCore.SQLServer.Models;

namespace VAN.SQLServerCore.SQLServer
{
    public class SQLServerInit(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserModel> UserDbContent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("user", "work");
        }
    }
}
