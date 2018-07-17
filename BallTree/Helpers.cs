using System;
using System.Linq;

namespace BallTree
{
    public class Helpers
    {
        public static void InformUser(int prediction, int actual)
        {
            Console.WriteLine($"I predict container number {prediction} will be empty");
            Console.WriteLine($"The empty container was number {actual}");

            if (prediction == actual)
            {
                Console.WriteLine("The prediction was correct");
            }
            else
            {
                Console.WriteLine("The prediction was incorrect");
            }
        }

        public static void ErrorMessage()
        {
            Console.WriteLine("Invalid argument passed, must be a positive integer");
        }

        public static void ContinueMessage()
        {
            Console.Write("Hit any key to continue: ");
            Console.Read();
        }

        public static int GetDepthFromArgs(string[] args)
        {
            if ((args == null) || (!args.Any())) return 0;

            string firstArg = args.First();
            bool ok = Int32.TryParse(firstArg, out int value);
            if (ok) return value;
            return 0;
        }
    }
}
