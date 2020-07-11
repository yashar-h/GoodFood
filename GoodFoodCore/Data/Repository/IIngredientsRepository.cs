using GoodFoodCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodFoodCore.Data.Repository
{
    public interface IIngredientsRepository
    {
        Task<List<Ingredient>> GetAll();
        Task<Ingredient> Get(string slug);
        Task Add(Ingredient ingredient);
        Task Update(Ingredient ingredient);
        Task Delete(Ingredient ingredient);
        bool Exists(string slug);
    }
}
