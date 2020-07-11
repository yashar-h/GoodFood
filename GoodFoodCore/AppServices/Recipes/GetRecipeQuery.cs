using System;
using GoodFoodCore.Common;
using GoodFoodCore.Data.Repository;
using GoodFoodCore.Models;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GoodFoodCore.AppServices.Recipes
{
    public sealed class GetRecipeQuery : IQuery<Recipe>
    {
        private readonly string _slug;

        public GetRecipeQuery(string slug)
        {
            _slug = slug;
        }

        internal sealed class GetRecipeQueryHandler : IQueryHandler<GetRecipeQuery, Recipe>
        {
            private readonly IRecipesRepository _recipesRepository;

            public GetRecipeQueryHandler([NotNull] IRecipesRepository recipesRepository)
            {
                _recipesRepository = recipesRepository ?? throw new ArgumentNullException(nameof(recipesRepository));
            }

            public async Task<Recipe> Handle(GetRecipeQuery query)
            {
                return await _recipesRepository.Get(query._slug);
            }
        }
    }
}