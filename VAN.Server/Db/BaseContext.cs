using Microsoft.EntityFrameworkCore;
using VueASPNet.Server.Models;

namespace VueASPNet.Server.Db
{
    public class BaseContext : DbContext
    {

        public DbSet<UserModel> UserDbContent { get; set; }

        public BaseContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("user", "work");
        }

    }
}