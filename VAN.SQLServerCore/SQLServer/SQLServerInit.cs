using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VAN.SQLServerCore.SQLServer.Models;

namespace VAN.SQLServerCore.SQLServer
{
    public class SQLServerInit : DbContext
    {
        public virtual DbSet<College> Colleges { get; set; }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Score> Scores { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }

        public string ConnectionString { get; init; } = default!;

        public SQLServerInit(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public SQLServerInit(DbContextOptions<SQLServerInit> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<College>().ToTable("college", "work").HasKey(c => c.CId);
            modelBuilder.Entity<Discipline>().ToTable("discipline", "work").HasKey(d => d.DId);
            modelBuilder.Entity<Profession>().ToTable("profession", "work").HasKey(p => p.PId);
            modelBuilder.Entity<School>().ToTable("school", "work").HasKey(s => s.SId);
            modelBuilder.Entity<Score>().ToTable("score", "work").HasKey(s => s.ScoreId);
            modelBuilder.Entity<Student>().ToTable("student", "work").HasKey(s => s.SId);
            modelBuilder.Entity<Teacher>().ToTable("teacher", "work").HasKey(t => t.TId);
            modelBuilder.Entity<User>().ToTable("user", "work").HasKey(u => u.Id);
            modelBuilder.Entity<Volunteer>().ToTable("volunteer", "work").HasKey(v => v.VId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {
            await using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<T>(sql, parameters);
        }
    }
}