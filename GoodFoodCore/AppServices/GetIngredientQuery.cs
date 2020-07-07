using GoodFoodCore.Common;
using GoodFoodCore.Repository;
using JetBrains.Annotations;

namespace GoodFoodCore.AppServices
{
    public sealed class GetIngredientQuery : IQuery<Ingredient>
    {
        public string Slug { get; }

        public GetIngredientQuery([NotNull] string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                throw new System.ArgumentNullException(nameof(slug));
            }

            Slug = slug;
        }

        internal sealed class GetIngredientQueryHandler : IQueryHandler<GetIngredientQuery, Ingredient>
        {
            private readonly IIngredientsRepository _ingredientsRepository;

            public GetIngredientQueryHandler(IIngredientsRepository ingredientsRepository)
            {
                _ingredientsRepository = ingredientsRepository;
            }

            public Ingredient Handle(GetIngredientQuery query)
            {
                return _ingredientsRepository.Get(query.Slug);
            }
        }
    }
}
