namespace MultiPayloadHandling.Models;

public class Pear : Fruit
{
    public override FruitType Type { get; } = FruitType.Pear;

    public int PearAttribute { get; set; }

    public string? PearName { get; set; }
}