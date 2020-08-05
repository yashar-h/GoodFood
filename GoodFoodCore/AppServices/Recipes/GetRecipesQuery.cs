using System;
using GoodFoodCore.Common;
using GoodFoodCore.Data.Repository;
using GoodFoodCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GoodFoodCore.AppServices.Recipes
{
    public sealed class GetRecipesQuery : IQuery<IEnumerable<Recipe>>
    {
        public GetRecipesQuery()
        {
        }

        internal sealed class GetRecipesQueryHandler : IQueryHandler<GetRecipesQuery, IEnumerable<Recipe>>
        {
            private readonly IRecipesRepository _recipesRepository;

            public GetRecipesQueryHandler([NotNull] IRecipesRepository recipesRepository)
            {
                _recipesRepository = recipesRepository ?? throw new ArgumentNullException(nameof(recipesRepository));
            }

            public async Task<IEnumerable<Recipe>> Handle(GetRecipesQuery query)
            {
                return await _recipesRepository.GetAll();
            }
        }
    }
}