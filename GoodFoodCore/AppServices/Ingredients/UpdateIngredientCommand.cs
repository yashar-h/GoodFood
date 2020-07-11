using GoodFoodCore.Common;
using GoodFoodCore.Data.Repository;
using GoodFoodCore.Models;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoodFoodCore.AppServices.Ingredients
{
    public sealed class UpdateIngredientCommand : ICommand
    {
        public string Title { get; }
        public string Description { get; }
        public string Slug { get; }

        public UpdateIngredientCommand([NotNull] string title, string description, [NotNull] string slug)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (string.IsNullOrEmpty(slug))
            {
                throw new ArgumentNullException(nameof(slug));
            }

            Title = title;
            Description = description;
            Slug = slug;
        }

        internal sealed class UpdateIngredientCommandHandler : ICommandHandler<UpdateIngredientCommand>
        {
            private readonly IIngredientsRepository _ingredientsRepository;

            public UpdateIngredientCommandHandler([NotNull] IIngredientsRepository ingredientsRepository)
            {
                _ingredientsRepository = ingredientsRepository ??
                                         throw new ArgumentNullException(nameof(ingredientsRepository));
            }

            public async Task<Result> Handle(UpdateIngredientCommand command)
            {
                var ingredient = new Ingredient(command.Title, command.Description, command.Slug);

                try
                {
                    await _ingredientsRepository.Update(ingredient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_ingredientsRepository.Exists(ingredient.Slug))
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
