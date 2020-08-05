using System;
using System.Threading;
using System.Threading.Tasks;
using GoodFood;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GoodFoodGrpcClient.RequestHandlers
{
    public class GetAllRecipes : BaseStrategy
    {
        public override async Task Execute(RecipeService.RecipeServiceClient client, IPrinter printer)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            using var streamingCall = client.RequestAllRecipes(new Empty(), cancellationToken: cts.Token);

            try
            {
                await foreach (var recipe in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
                {
                    printer.PrintRecipe(recipe);
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                printer.PrintLine("Stream cancelled.");
            }
        }
    }
}
