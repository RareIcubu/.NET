using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Lab1TestProject1"), InternalsVisibleTo("KnackpackGUI")]
namespace Lab1Knacpack;

public class Problem
{
    private int itemNumber = 0;
    private Item[] items;

    public Problem(int n, int seed)
    {
        Random random = new Random(seed);
        itemNumber = n;
        items =  new Item[itemNumber];

        for (int i = 0; i < itemNumber; i++)
        {
            items[i] = new Item();
            items[i].Id = i;
            items[i].Weight = random.Next(1,11);
            items[i].Value = random.Next(1,11);
            Console.WriteLine(items[i].Id + " " + items[i].Weight + " " + items[i].Value);
        }
        
    }

    internal Result Solve(int capacity)
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