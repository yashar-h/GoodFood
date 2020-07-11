using GoodFoodCore.Models;
using System.Linq;

namespace GoodFoodCore.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RecipeContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Recipes.Any())
            {
                return;   // DB has been seeded
            }

            var recipes = new Recipe[]
            {
                new Recipe{Slug="cake", Description="Chocolate cake", Category=Recipe.RecipeCategory.Dessert,Title="Cake"},
                new Recipe{Slug="burger", Description="delicious burger", Category=Recipe.RecipeCategory.MainCourse,Title="Burger"},
                new Recipe{Slug="salad", Description="Cesar salad", Category=Recipe.RecipeCategory.Starters,Title="Salad"}
            };
            foreach (Recipe r in recipes)
            {
                context.Recipes.Add(r);
            }
            context.SaveChanges();

            var ingredients = new Ingredient[]
            {
                new Ingredient("Salt","To make food tasty!","salt"),
                new Ingredient("Pepper", "To make food spicy!", "pepper"),
                new Ingredient("Sugar", "To make food sweet!", "Sugar"),
                new Ingredient("Tomato", "A round red vegetable.", "tomato")
            };
            foreach (Ingredient i in ingredients)
            {
                context.Ingredients.Add(i);
            }
            context.SaveChanges();

            var recipeIngredients = new RecipeIngredient[]
            {
                new RecipeIngredient{RecipeSlug="cake", IngredientSlug="sugar", Amount="A lot!"},
                new RecipeIngredient{RecipeSlug="cake", IngredientSlug="salt", Amount="1 teaspoon"},
                new RecipeIngredient{RecipeSlug="salad", IngredientSlug="tomato", Amount="two pieces"},
                new RecipeIngredient{RecipeSlug="burger", IngredientSlug="pepper", Amount="just a bit"},
                new RecipeIngredient{RecipeSlug="burger", IngredientSlug="tomato", Amount="3 slices"}
            };
            foreach (RecipeIngredient re in recipeIngredients)
            {
                context.RecipeIngredients.Add(re);
            }
            context.SaveChanges();
        }
    }
}
