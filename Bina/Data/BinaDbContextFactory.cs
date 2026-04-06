using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Bina.DAL.Data;

namespace Bina.Data
{
    public class BinaDbContextFactory : IDesignTimeDbContextFactory<BinaDbContext>
    {
        public BinaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BinaDbContext>();
            // Use localdb
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BinaAzDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new BinaDbContext(optionsBuilder.Options);
        }
    }
}