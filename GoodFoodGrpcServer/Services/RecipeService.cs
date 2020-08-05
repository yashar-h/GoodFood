using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodFood;
using GoodFoodCore.AppServices.Recipes;
using GoodFoodCore.Utils;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace GoodFoodGrpcServer.Services
{
    public class RecipeService : GoodFood.RecipeService.RecipeServiceBase
    {
        private readonly Dispatcher _dispatcher;
        private readonly ILogger<RecipeService> _logger;

        public RecipeService([NotNull] Dispatcher dispatcher, ILogger<RecipeService> logger)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _logger = logger;
        }

        public override async Task RequestAllRecipes(Empty request, IServerStreamWriter<Recipe> responseStream, ServerCallContext context)
        {
            var recipes = await _dispatcher.Dispatch(new GetRecipesQuery());

            foreach (var recipe in recipes)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }

                //await Task.Delay(1000); // Gotta look busy

                _logger.LogInformation("Sending Recipe response");

                await responseStream.WriteAsync(ConvertDomainToDto(recipe));
            }
        }

        public override async Task<Recipe> RequestRecipe(RecipeRequest request, ServerCallContext context)
        {
            var recipe = await _dispatcher.Dispatch(new GetRecipeQuery(request.Slug));
            return ConvertDomainToDto(recipe);
        }

        private Recipe ConvertDomainToDto(GoodFoodCore.Models.Recipe domainRecipe)
        {
            var recipe = new Recipe
            {
                Title = domainRecipe.Title,
                Description = domainRecipe.Description,
                Category = (Recipe.Types.Category)System.Enum.Parse(typeof(Recipe.Types.Category),
                    domainRecipe.Category.ToString(), true),
                Slug = domainRecipe.Slug,
                RecipeIngredient = { new List<RecipeIngredient>() }
            };

            if (domainRecipe.RecipeIngredients == null || !domainRecipe.RecipeIngredients.Any())
            {
                return recipe;
            }

            foreach (var domainRecipeRecipeIngredient in domainRecipe.RecipeIngredients)
            {
                recipe.RecipeIngredient.Add(new RecipeIngredient
                {
                    Amount = domainRecipeRecipeIngredient.Amount,
                    Id = domainRecipeRecipeIngredient.ID,
                    Ingredient = new Ingredient
                    {
                        Title = domainRecipeRecipeIngredient.Ingredient.Title,
                        Description = domainRecipeRecipeIngredient.Ingredient.Description,
                        Slug = domainRecipeRecipeIngredient.IngredientSlug
                    }
                });
            }

            return recipe;
        }
    }
}