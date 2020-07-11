using System;
using GoodFoodCore.Common;
using GoodFoodCore.Data.Repository;
using GoodFoodCore.Models;
using JetBrains.Annotations;
using System.Threading.Tasks;

namespace GoodFoodCore.AppServices.Ingredients
{
    public sealed class GetIngredientQuery : IQuery<Ingredient>
    {
        public string Slug { get; }

        public GetIngredientQuery([NotNull] string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                throw new ArgumentNullException(nameof(slug));
            }

            Slug = slug;
        }

        internal sealed class GetIngredientQueryHandler : IQueryHandler<GetIngredientQuery, Ingredient>
        {
            private readonly IIngredientsRepository _ingredientsRepository;

            public GetIngredientQueryHandler([NotNull] IIngredientsRepository ingredientsRepository)
            {
                _ingredientsRepository = ingredientsRepository ?? throw new ArgumentNullException(nameof(ingredientsRepository));
            }

            public async Task<Ingredient> Handle(GetIngredientQuery query)
            {
                return await _ingredientsRepository.Get(query.Slug);
            }
        }
    }
}
