using System;
using System.Threading.Tasks;
using GoodFoodCore.Common;
using GoodFoodCore.Data.Repository;
using GoodFoodCore.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace GoodFoodCore.AppServices.Recipes
{
    public sealed class UpdateRecipeCommand : ICommand
    {
        public Recipe Recipe { get; }

        public UpdateRecipeCommand(Recipe recipe)
        {
            Recipe = recipe;
        }

        internal sealed class UpdateRecipeCommandHandler : ICommandHandler<UpdateRecipeCommand>
        {
            private readonly IRecipesRepository _recipesRepository;

            public UpdateRecipeCommandHandler([NotNull] IRecipesRepository recipesRepository)
            {
                _recipesRepository = recipesRepository ?? throw new ArgumentNullException(nameof(recipesRepository));
            }

            public async Task<Result> Handle(UpdateRecipeCommand command)
            {
                try
                {
                    await _recipesRepository.Update(command.Recipe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_recipesRepository.Exists(command.Recipe.Slug))
                    {
                        return Result.Fail("Recipe not found.");
                    }
                    else
                    {
                        throw;
                    }
                }

                return Result.Ok();
            }
        }
    }
}
