using System.Threading.Tasks;
using GoodFood;

namespace GoodFoodGrpcClient.RequestHandlers
{
    public abstract class BaseStrategy
    {
        public virtual Task Execute(RecipeService.RecipeServiceClient client, IPrinter printer)
        {
            return Task.CompletedTask;
        }
    }
}
