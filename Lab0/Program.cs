using System;
namespace Lab0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number:");
            int topNumber = int.Parse(Console.ReadLine());
            FizzBuzz fizzBuzz = new FizzBuzz(topNumber);
            fizzBuzz.Run();
        }
    }
}