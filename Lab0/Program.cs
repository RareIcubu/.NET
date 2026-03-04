using System;
namespace Lab0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number:");
            int TopNumber = int.Parse(Console.ReadLine());
            FizzBuzz fizzBuzz = new FizzBuzz(TopNumber);
            fizzBuzz.Run();
        }
    }
}