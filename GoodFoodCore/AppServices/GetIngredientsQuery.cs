using GoodFoodCore.Common;
using GoodFoodCore.Repository;
using System.Collections.Generic;
using System.Linq;

namespace GoodFoodCore.AppServices
{
    public sealed class GetIngredientsQuery : IQuery<List<Ingredient>>
    {
        public GetIngredientsQuery()
        {
        }

        internal sealed class GetIngredientsQueryHandler : IQueryHandler<GetIngredientsQuery, List<Ingredient>>
        {
            private readonly IIngredientsRepository _ingredientsRepository;

            public GetIngredientsQueryHandler(IIngredientsRepository ingredientsRepository)
            {
                _ingredientsRepository = ingredientsRepository;
            }

            public List<Ingredient> Handle(GetIngredientsQuery query)
            {
                return _ingredientsRepository.GetAll().ToList();
            }
        }
    }
}
