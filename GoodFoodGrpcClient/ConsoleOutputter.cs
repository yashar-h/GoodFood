using System;

namespace GoodFoodGrpcClient
{
    public class ConsoleOutputter : IOutputter
    {
        public void Write(string input)
        {
            Console.WriteLine(input);
        }
    }

    public interface IOutputter
    {
        void Write(string input);
    }
}
