using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GoodFoodCore.Data.Repository
{
    /// <summary>
    /// Used for Migrations
    /// </summary>
    public class BRecipeContextFactory : IDesignTimeDbContextFactory<RecipeContext>
    {
        private const string _defaultConnectionString
            = "Server=(localdb)\\mssqllocaldb;Database=GoodFood;Trusted_Connection=True;MultipleActiveResultSets=true";

        public RecipeContext CreateDbContext(string[] args)
        {
            var connectionString = args != null && args.Length > 0 ? args[0] : _defaultConnectionString;

            var optionsBuilder = new DbContextOptionsBuilder<RecipeContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new RecipeContext(optionsBuilder.Options);
        }
    }
}
