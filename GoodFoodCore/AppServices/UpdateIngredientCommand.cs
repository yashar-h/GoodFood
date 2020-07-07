using GoodFoodCore.Common;
using GoodFoodCore.Repository;
using JetBrains.Annotations;
using System;

namespace GoodFoodCore.AppServices
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

            public UpdateIngredientCommandHandler(IIngredientsRepository ingredientsRepository)
            {
                _ingredientsRepository = ingredientsRepository;
            }

            public Result Handle(UpdateIngredientCommand command)
            {
                var ingredient = new Ingredient(command.Title, command.Description, command.Slug);
                _ingredientsRepository.Update(ingredient);

                return Result.Ok();
            }
        }
    }
}
