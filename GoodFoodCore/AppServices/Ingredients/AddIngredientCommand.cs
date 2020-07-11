using GoodFoodCore.Common;
using GoodFoodCore.Data.Repository;
using GoodFoodCore.Models;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoodFoodCore.AppServices.Ingredients
{
    public sealed class AddIngredientCommand : ICommand
    {
        public string Title { get; }
        public string Description { get; }
        public string Slug { get; }

        public AddIngredientCommand([NotNull] string title, string description, [NotNull] string slug)
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

        internal sealed class AddIngredientCommandHandler : ICommandHandler<AddIngredientCommand>
        {
            private readonly IIngredientsRepository _ingredientsRepository;

            public AddIngredientCommandHandler([NotNull] IIngredientsRepository ingredientsRepository)
            {
                _ingredientsRepository = ingredientsRepository ?? throw new ArgumentNullException(nameof(ingredientsRepository));
            }

            public async Task<Result> Handle(AddIngredientCommand command)
            {
                var ingredient = new Ingredient(command.Title, command.Description, command.Slug);

                try
                {
                    await _ingredientsRepository.Add(ingredient);
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    return Result.Fail("Unable to save changes. " +
                                       "Try again, and if the problem persists " +
                                       "see your system administrator.");
                }

                return Result.Ok();
            }
        }
    }
}
