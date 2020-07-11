using GoodFoodCore.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodWeb.Models
{
    public class RecipeIngredientViewModel
    {
        public int ID { get; set; }
        public IngredientViewModel Ingredient { get; set; }

        [Required]
        public string Amount { get; set; }

        public RecipeIngredientViewModel()
        {
        }

        public RecipeIngredientViewModel(RecipeIngredient recipeIngredient)
        {
            ID = recipeIngredient.ID;
            Amount = recipeIngredient.Amount;

            if(recipeIngredient.Ingredient != null)
            {
                Ingredient = new IngredientViewModel(recipeIngredient.Ingredient);
            }
        }

        public RecipeIngredient ToDomainModel(Recipe recipe)
        {
            var ri = new RecipeIngredient();
            ri.ID = ID;
            ri.Amount = Amount;
            ri.IngredientSlug = Ingredient.Slug;
            ri.Ingredient = Ingredient.ToDomainModel();
            ri.RecipeSlug = recipe.Slug;
            ri.Recipe = recipe;

            return ri;
        }
    }
}
