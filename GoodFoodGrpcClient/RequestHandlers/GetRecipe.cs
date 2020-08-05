using System;
using System.Threading.Tasks;
using GoodFood;

namespace GoodFoodGrpcClient.RequestHandlers
{
    public class GetRecipe : BaseStrategy
    {
        private readonly IInputter _inputter;

        public GetRecipe(IInputter inputter)
        {
            _inputter = inputter ?? throw new ArgumentNullException(nameof(inputter));
        }

        public override async Task Execute(RecipeService.RecipeServiceClient client, IPrinter printer)
        {
            printer.PrintLine("which recipe would you like to see?");
            var recipeSlug = _inputter.Read().ToLower();
            var reply = await client.RequestRecipeAsync(new RecipeRequest { Slug = recipeSlug });
            if (reply != null)
            {
                printer.PrintRecipe(reply);
            }
            else
            {
                printer.PrintLine($"recipe with name {recipeSlug} not found.");
            }
        }
    }
}
