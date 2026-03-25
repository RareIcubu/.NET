using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Lab1TestProject1")]
namespace Lab1Knacpack;

internal class Item
{
    public int Id { get; set; }
    public int Value { get; set; }
    public int Weight { get; set; }
}