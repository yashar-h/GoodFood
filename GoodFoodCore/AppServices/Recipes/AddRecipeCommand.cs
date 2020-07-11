using System;
using System.Threading.Tasks;
using GoodFoodCore.Common;
using GoodFoodCore.Data.Repository;
using GoodFoodCore.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace GoodFoodCore.AppServices.Recipes
{
    public sealed class AddRecipeCommand : ICommand
    {
        public string Title { get; }
        public string Description { get; }
        public string Slug { get; }
        public Recipe.RecipeCategory Category { get; }

        public AddRecipeCommand([NotNull] string title, string description, [NotNull] string slug, Recipe.RecipeCategory category)
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
            Category = category;
        }

        internal sealed class AddRecipeCommandHandler : ICommandHandler<AddRecipeCommand>
        {
            private readonly IRecipesRepository _recipesRepository;

            public AddRecipeCommandHandler([NotNull] IRecipesRepository recipesRepository)
            {
                _recipesRepository = recipesRepository ?? throw new ArgumentNullException(nameof(recipesRepository));
            }

            public async Task<Result> Handle(AddRecipeCommand command)
            {
                var recipe = new Recipe(command.Title, command.Description, command.Slug, command.Category);

                try
                {
                    await _recipesRepository.Add(recipe);
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
