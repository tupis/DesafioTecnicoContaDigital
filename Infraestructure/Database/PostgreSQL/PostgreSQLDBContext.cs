using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Database.PostgreSQL
{
    public class PostgreSQLDBContext : DbContext
    {
        public PostgreSQLDBContext(DbContextOptions<PostgreSQLDBContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().AddUserSecrets("2c4c9521-3135-466e-afd3-a5ad8aa5c2d0").Build();
            var connString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}