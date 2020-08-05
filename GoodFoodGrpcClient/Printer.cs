using System;
using System.Linq;
using GoodFood;

namespace GoodFoodGrpcClient
{
    public interface IPrinter
    {
        void PrintLine(string input);
        void PrintRecipe(Recipe recipe);
        void PrintRecipeIngredient(RecipeIngredient recipeIngredient, string indentation);
        void PrintWelcomeNote();
    }

    public class Printer : IPrinter
    {
        private readonly IOutputter _outputter;

        public Printer(IOutputter outputter)
        {
            _outputter = outputter ?? throw new ArgumentNullException(nameof(outputter));
        }

        public void PrintLine(string input)
        {
            _outputter.Write(input);
        }

        public void PrintRecipe(Recipe recipe)
        {
            _outputter.Write($"{recipe.Title} ({recipe.Description})");
            _outputter.Write($"    Category: {recipe.Category}");

            if (recipe.RecipeIngredient != null && recipe.RecipeIngredient.Any())
            {
                _outputter.Write($"    Ingredients:");
                foreach (var recipeIngredient in recipe.RecipeIngredient)
                {
                    PrintRecipeIngredient(recipeIngredient, "        ");
                }
            }

            _outputter.Write(string.Empty);
        }

        public void PrintRecipeIngredient(RecipeIngredient recipeIngredient, string indentation)
        {
            _outputter.Write($"{indentation}{recipeIngredient.Ingredient.Title}({recipeIngredient.Ingredient.Description})");
            _outputter.Write($"{indentation}{recipeIngredient.Amount}");
            _outputter.Write($"{indentation}-------------------");
        }

        public void PrintWelcomeNote()
        {
            _outputter.Write(@"
Write the index of the command you'd like to execute:

1: Get all recipes
2: Get recipe

0: Exit
");
        }
    }
}
