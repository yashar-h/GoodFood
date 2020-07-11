using System;
using System.Threading.Tasks;
using GoodFoodCore.Common;
using GoodFoodCore.Data.Repository;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace GoodFoodCore.AppServices.Recipes
{
    public sealed class DeleteRecipeCommand : ICommand
    {
        public string Slug{ get; }

        public DeleteRecipeCommand(string slug)
        {
            Slug = slug;
        }

        internal sealed class DeleteRecipeCommandHandler : ICommandHandler<DeleteRecipeCommand>
        {
            private readonly IRecipesRepository _recipesRepository;

            public DeleteRecipeCommandHandler([NotNull] IRecipesRepository recipesRepository)
            {
                _recipesRepository = recipesRepository ?? throw new ArgumentNullException(nameof(recipesRepository));
            }

            public async Task<Result> Handle(DeleteRecipeCommand command)
            {
                try
                {
                    var recipe = await _recipesRepository.Get(command.Slug);
                    if (recipe == null)
                    {
                        return Result.Fail("Recipe not found.");
                    }

                    await _recipesRepository.Delete(recipe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_recipesRepository.Exists(command.Slug))
                    {
                        return Result.Fail("Recipe not found.");
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException /* ex */)
                {
                    return Result.Fail("Delete failed. Try again, and if the problem persists " +
                                       "see your system administrator.");
                }

                return Result.Ok();
            }
        }
    }
}
