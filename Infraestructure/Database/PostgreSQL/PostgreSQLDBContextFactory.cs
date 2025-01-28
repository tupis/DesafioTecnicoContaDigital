using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Database.PostgreSQL
{
    class PostgreSQLDBContextFactory : IDesignTimeDbContextFactory<PostgreSQLDBContext>
    {
        public PostgreSQLDBContextFactory() { }

        public PostgreSQLDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PostgreSQLDBContext>();
            return new PostgreSQLDBContext(builder.Options);
        }
    }
}
