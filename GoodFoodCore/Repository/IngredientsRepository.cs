using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodFoodCore.Repository
{
    public class InMemoryIngredientsRepository : IIngredientsRepository
    {
        internal List<Ingredient> Ingredients;

        public InMemoryIngredientsRepository()
        {
            Ingredients = new List<Ingredient>()
            {
                new Ingredient("Salt", "To make food salty.", "salt"),
                new Ingredient("Pepper", "To make food spicy.", "pepper"),
                new Ingredient("Sugar", "To make food sweet.", "sugar")
            };
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return Ingredients.AsReadOnly();
        }

        public Ingredient Get(string slug)
        {
            return Ingredients.FirstOrDefault(i => i.Slug == slug);
        }

        public void Add(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void Update(Ingredient ingredient)
        {
            var storedIngredient = Ingredients.FirstOrDefault(i => i.Slug == ingredient.Slug);

            if (storedIngredient != null)
            {
                storedIngredient.Title = ingredient.Title;
                storedIngredient.Description = ingredient.Description;
            }
        }

        public void Remove(string slug)
        {
            Ingredients.RemoveAll(i => i.Slug == slug);
        }
    }
}
