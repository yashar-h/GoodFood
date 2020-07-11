using GoodFoodCore.Common;
using GoodFoodCore.Data.Repository;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoodFoodCore.AppServices.Ingredients
{
    public sealed class DeleteIngredientCommand : ICommand
    {
        public string Slug { get; }

        public DeleteIngredientCommand([NotNull] string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                throw new ArgumentNullException(nameof(slug));
            }

            Slug = slug;
        }

        internal sealed class DeleteIngredientCommandHandler : ICommandHandler<DeleteIngredientCommand>
        {
            private readonly IIngredientsRepository _ingredientsRepository;

            public DeleteIngredientCommandHandler([NotNull] IIngredientsRepository ingredientsRepository)
            {
                _ingredientsRepository = ingredientsRepository ?? throw new ArgumentNullException(nameof(ingredientsRepository));
            }

            public async Task<Result> Handle(DeleteIngredientCommand command)
            {
                try
                {
                    var ingredient = await _ingredientsRepository.Get(command.Slug);
                    if (ingredient == null)
                    {
                        return Result.Fail("Ingredient not found.");
                    }

                    await _ingredientsRepository.Delete(ingredient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_ingredientsRepository.Exists(command.Slug))
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
