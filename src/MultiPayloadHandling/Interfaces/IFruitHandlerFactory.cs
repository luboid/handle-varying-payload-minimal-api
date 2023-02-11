namespace MultiPayloadHandling.Interfaces;

public interface IFruitHandlerFactory
{
    public IFruitHandler Create(FruitType fruitType);
}