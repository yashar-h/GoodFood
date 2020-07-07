using System;
using System.Collections.Generic;
using System.Text;

namespace GoodFoodCore.Repository
{
    public interface IIngredientsRepository
    {
        IEnumerable<Ingredient> GetAll();
        Ingredient Get(string slug);
        void Add(Ingredient ingredient);
        void Update(Ingredient ingredient);
        void Remove(string slug);
    }
}
