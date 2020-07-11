using GoodFoodCore.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoodFoodCore.Data.Repository
{
    public class SqlServerIngredientsRepository : IIngredientsRepository
    {
        private readonly RecipeContext _context;

        public SqlServerIngredientsRepository(RecipeContext context)
        {
            _context = context;
        }

        public async Task Add(Ingredient ingredient)
        {
            _context.Add(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task<Ingredient> Get(string slug)
        {
            return await _context.Ingredients
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Slug == slug);
        }

        public async Task<List<Ingredient>> GetAll()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task Delete(Ingredient ingredient)
        {
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Ingredient ingredient)
        {
            _context.Update(ingredient);
            await _context.SaveChangesAsync();
        }

        public bool Exists(string slug)
        {
            return _context.Ingredients.Any(e => e.Slug == slug);
        }
    }
}
