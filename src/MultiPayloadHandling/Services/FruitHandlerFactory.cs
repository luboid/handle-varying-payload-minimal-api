namespace MultiPayloadHandling.Interfaces;

public class FruitHandlerFactory : IFruitHandlerFactory
{
    private readonly IServiceProvider serviceProvider;
    private readonly FruitServiceTypeMap fruitTypeMap;

    public FruitHandlerFactory(IServiceProvider serviceProvider, FruitServiceTypeMap fruitTypeMap)
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