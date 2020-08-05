using GoodFoodCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GoodFoodCore.Data.Repository
{
    public class SqlServerRecipesRepository : IRecipesRepository
    {
        private readonly RecipeContext _context;

        public SqlServerRecipesRepository(RecipeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            return _context.Recipes.AsNoTracking();
        }

        public async Task<Recipe> Get(string slug)
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(re => re.Ingredient)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Slug == slug);
        }

        public async Task Add(Recipe recipe)
        {
            _context.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Recipe recipe)
        {
            _context.Update(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        public bool Exists(string slug)
        { 
            return _context.Recipes.Any(e => e.Slug == slug);
        }
    }
}
