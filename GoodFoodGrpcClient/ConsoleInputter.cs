using System;

namespace GoodFoodGrpcClient
{
    public interface IInputter
    {
        string Read();
    }

    public class ConsoleInputter : IInputter
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
