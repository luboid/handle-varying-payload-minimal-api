namespace MultiPayloadHandling.Services;

[AttributeUsage(AttributeTargets.Class)]
public class FruitSreviceAttribute : Attribute
{
    public FruitSreviceAttribute(FruitType fruitType)
    {
        FruitType = fruitType;
    }

    public FruitType FruitType { get; }
}