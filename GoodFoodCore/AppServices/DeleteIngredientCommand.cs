using GoodFoodCore.Common;
using GoodFoodCore.Repository;
using JetBrains.Annotations;
using System;

namespace GoodFoodCore.AppServices
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

            public DeleteIngredientCommandHandler(IIngredientsRepository ingredientsRepository)
            {
                _ingredientsRepository = ingredientsRepository;
            }

            public Result Handle(DeleteIngredientCommand command)
            {
                _ingredientsRepository.Remove(command.Slug);

                return Result.Ok();
            }
        }
    }
}
