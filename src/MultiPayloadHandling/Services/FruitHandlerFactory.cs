namespace MultiPayloadHandling.Interfaces;

public class FruitHandlerFactory : IFruitHandlerFactory
{
    private readonly IServiceProvider serviceProvider;
    private readonly FruitTypeMap fruitTypeMap;

    public FruitHandlerFactory(IServiceProvider serviceProvider, FruitTypeMap fruitTypeMap)
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

        throw new ArgumentOutOfRangeException(nameof(handler));
    }
}