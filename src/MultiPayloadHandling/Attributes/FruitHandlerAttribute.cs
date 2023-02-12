namespace MultiPayloadHandling.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class FruitHandlerAttribute : Attribute
{
    public FruitHandlerAttribute(FruitType fruitType)
    {
        FruitType = fruitType;
    }

    public FruitType FruitType { get; }
}