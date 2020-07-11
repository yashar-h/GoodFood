using GoodFoodCore.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static GoodFoodCore.Models.Recipe;

namespace GoodFoodWeb.Models
{
    public class RecipeViewModel
    {
        [Required]
        [MaxLength(90, ErrorMessage = "Max length is 90 characters.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max length is 50 characters.")]
        public string Slug { get; set; }

        public RecipeCategory Category { get; set; }

        public ICollection<RecipeIngredientViewModel> RecipeIngredients { get; set; }

        public RecipeViewModel() { }

        public RecipeViewModel(Recipe recipe)
        {
            Title = recipe.Title;
            Description = recipe.Description;
            Slug = recipe.Slug;
            Category = recipe.Category;

            RecipeIngredients = new List<RecipeIngredientViewModel>();
            if (recipe.RecipeIngredients != null && recipe.RecipeIngredients.Any())
            {
                var recipeIngredients = recipe.RecipeIngredients.Select(
                        recipeIngredient => new RecipeIngredientViewModel(recipeIngredient));
                foreach(var recipeIngredient in recipeIngredients)
                {
                    RecipeIngredients.Add(recipeIngredient);
                }
            }

        }

        public Recipe ToDomainModel()
        {
            var recipe = new Recipe();
            recipe.Slug = Slug;
            recipe.Title = Title;
            recipe.Description = Description;
            recipe.Category = Category;

            recipe.RecipeIngredients = new Collection<RecipeIngredient>();
            if (RecipeIngredients != null && RecipeIngredients.Any())
            {
                var recipeIngredients = RecipeIngredients.Select(ri => ri.ToDomainModel(recipe));
                foreach (var recipeIngredient in recipeIngredients)
                {
                    recipe.RecipeIngredients.Add(recipeIngredient);
                }
            }

            return recipe;
        }
    }
}
