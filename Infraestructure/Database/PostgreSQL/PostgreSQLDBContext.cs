using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Database.PostgreSQL
{
    public class PostgreSQLDBContext : DbContext
    {
        public PostgreSQLDBContext(DbContextOptions<PostgreSQLDBContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().AddUserSecrets("b29dbef5-4977-4d50-81e8-3a6bf331b84b").Build();
            var connString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasOne(u => u.User).WithOne(a => a.Account).HasForeignKey<Account>(a => a.UserId);
            base.OnModelCreating(modelBuilder);
        }
    }
}