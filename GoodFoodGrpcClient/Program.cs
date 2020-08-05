using System;
using System.Threading.Tasks;
using GoodFood;
using GoodFoodGrpcClient.RequestHandlers;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace GoodFoodGrpcClient
{
    class Program
    {
        private static Printer _printer;
        private static IInputter _inputter;
        private static IOutputter _outputter;
        private static IConfiguration _config;

        static async Task Main(string[] args)
        {
            InitializeApplication();

            // The server address must match the address of the gRPC server.
            using var channel = GrpcChannel.ForAddress(_config["ServerAddress"]);
            var client = new RecipeService.RecipeServiceClient(channel);

            var exiting = false;
            while (!exiting)
            {
                _printer.PrintWelcomeNote();
                var index = ReadUserInput(ref exiting);
                var requestHandlerStrategy = PickStrategy(index);
                await requestHandlerStrategy.Execute(client, _printer);
            }
        }

        private static int ReadUserInput(ref bool exiting)
        {
            //Read user input
            var indexText = Console.ReadLine();
            if (!int.TryParse(indexText, out int index)) return -1;
            if (index == 0) exiting = true;
            return index;
        }

        private static void InitializeApplication()
        {
            ReadConfigurationFile();
            _inputter = new ConsoleInputter();
            _outputter = new ConsoleOutputter();
            _printer = new Printer(_outputter);
        }

        private static void ReadConfigurationFile()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }

        private static BaseStrategy PickStrategy(int index)
        {
            return index switch
            {
                1 => new GetAllRecipes(),
                2 => new GetRecipe(_inputter),
                _ => new DefaultStrategy()
            };
        }

    }
}
