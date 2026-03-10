using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SolutionTest"), InternalsVisibleTo("KnackPackGUI")]

namespace ConsoleApp1;

internal class Result
{
    public Result(List<int> indecies, int totalWeight, int totalValue)
    {
        Id = indecies;
        Value = totalValue;
        Weight = totalWeight;
    }

    public List<int> Id { get; set; }
    public int Value { get; set; }
    public int Weight { get; set; }

    public override string ToString()
    {
        return $"items: {string.Join(" ", Id)}\ntotal value: {Value}\ntotal weight: {Weight}";
    }
}