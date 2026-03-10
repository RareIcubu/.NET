using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SolutionTest"), InternalsVisibleTo("KnackpackGUI")]

namespace ConsoleApp1;

internal class Problem
{
    private readonly int itemNumber;
    private readonly Item[] items;

    public Problem(int n, int seed)
    {
        itemNumber = n;
        items = new Item[n];
        var random = new Random(seed);
        for (var i = 0; i < itemNumber; i++)
            items[i] = new Item
            {
                Id = i,
                Weight = random.Next(1, 11),
                Value = random.Next(1, 11)
            };
    }

    public Result Solve(int capacity)
    {
        Array.Sort(items, (a, b) =>
        {
            var RatioA = (double)a.Value / a.Weight;
            var RatioB = (double)b.Value / b.Weight;
            return RatioB.CompareTo(RatioA);
        });
        var C = 0;
        var indecies = new List<int>();
        var totalWeight = 0;
        var totalValue = 0;
        for (var i = 0; i < itemNumber; i++)
        {
            var item = items[i];
            if (totalWeight + item.Weight > capacity) continue;
            indecies.Add(item.Id);
            totalWeight += item.Weight;
            totalValue += item.Value;
        }

        var result = new Result(indecies, totalWeight, totalValue);
        return result;
    }
}