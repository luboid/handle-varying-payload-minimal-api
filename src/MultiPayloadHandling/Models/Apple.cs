namespace MultiPayloadHandling.Models;

public class Apple : Fruit
{
    public override FruitType Type { get; } = FruitType.Apple;

    public int AppleAttribute { get; set; }

    public string? AppleName { get; set; }
}