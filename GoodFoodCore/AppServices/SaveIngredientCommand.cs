using GoodFoodCore.Common;
using GoodFoodCore.Repository;
using JetBrains.Annotations;
using System;

namespace GoodFoodCore.AppServices
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

            public AddIngredientCommandHandler(IIngredientsRepository ingredientsRepository)
            {
                _ingredientsRepository = ingredientsRepository;
            }

            public Result Handle(AddIngredientCommand command)
            {
                var ingredient = new Ingredient(command.Title, command.Description, command.Slug);
                _ingredientsRepository.Add(ingredient);

                return Result.Ok();
            }
        }
    }
}
