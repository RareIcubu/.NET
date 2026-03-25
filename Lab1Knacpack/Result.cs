using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Lab1TestProject1"), InternalsVisibleTo("KnackpackGUI")]
namespace Lab1Knacpack;

internal class Result
{
   public List<int> Indecies { get; set; }
   public int TotalWeight { get; set; }
   public int TotalValue { get; set; }

   public Result(List<int> indecies, int totalWeight, int totalValue)
   {
      Indecies = indecies;
      TotalWeight = totalWeight;
      TotalValue = totalValue;
   }

   public override string ToString()
   {
      return $"items: {string.Join(" ", Indecies)}\ntotal value: {TotalValue}\ntotal weight: {TotalWeight}";
   }
}