namespace MultiPayloadHandling.Interfaces;

public class FruitHandlerFactory : IFruitHandlerFactory
{
    private readonly IServiceProvider serviceProvider;
    private readonly FruitHandlerTypeMap fruitTypeMap;

    public FruitHandlerFactory(IServiceProvider serviceProvider, FruitHandlerTypeMap fruitTypeMap)
    {
        this.serviceProvider = serviceProvider;
        this.fruitTypeMap = fruitTypeMap;
    }

    public IFruitHandler Create(FruitType fruitType)
    {
        if (fruitTypeMap.TryGetValue(fruitType, out var handler)) 
        {
            return (IFruitHandler)serviceProvider.GetRequiredService(handler);
        }

        throw new ArgumentOutOfRangeException(nameof(fruitType));
    }
}