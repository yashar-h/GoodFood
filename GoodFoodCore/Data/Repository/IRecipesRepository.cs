using GoodFoodCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodFoodCore.Data.Repository
{
    public interface IRecipesRepository
    {
        Task<IEnumerable<Recipe>> GetAll();
        Task<Recipe> Get(string slug);
        Task Add(Recipe recipe);
        Task Update(Recipe recipe);
        Task Delete(Recipe recipe);
        bool Exists(string slug);
    }
}
