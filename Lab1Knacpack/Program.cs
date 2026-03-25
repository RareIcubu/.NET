namespace Lab1Knacpack;

class Program
{
    static void Main(string[] args)
    {
        var n = 0;
        var seed = 0;
        var capacity = 0;
        Console.WriteLine("Enter the number of items: ");
        n = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the seed: ");
        seed = int.Parse(Console.ReadLine());
        var problem = new Problem(n, seed);
        Console.WriteLine("Enter the capacity: ");
        capacity = int.Parse(Console.ReadLine());
        var result = problem.Solve(capacity);
        Console.WriteLine(result.ToString());
 
    }
}