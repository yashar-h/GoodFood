using System;
using GoodFoodCore.Common;
using GoodFoodCore.Data.Repository;
using GoodFoodCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GoodFoodCore.AppServices.Ingredients
{
    public sealed class GetIngredientsQuery : IQuery<List<Ingredient>>
    {
        public GetIngredientsQuery()
        {
        }

        internal sealed class GetIngredientsQueryHandler : IQueryHandler<GetIngredientsQuery, List<Ingredient>>
        {
            private readonly IIngredientsRepository _ingredientsRepository;

            public GetIngredientsQueryHandler([NotNull] IIngredientsRepository ingredientsRepository)
            {
                _ingredientsRepository = ingredientsRepository ?? throw new ArgumentNullException(nameof(ingredientsRepository));
            }

            public async Task<List<Ingredient>> Handle(GetIngredientsQuery query)
            {
                return await _ingredientsRepository.GetAll();
            }
        }
    }
}
