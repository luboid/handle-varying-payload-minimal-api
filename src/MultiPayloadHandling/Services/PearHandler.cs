namespace MultiPayloadHandling.Services;

[FruitHandler(FruitType.Pear)]
public class PearHandler : IPearHandler
{
    private readonly IPearJuiceHandler pearJuiceHandler;

    public PearHandler(IPearJuiceHandler pearJuiceHandler)
    {
        this.pearJuiceHandler = pearJuiceHandler;
    }

    public async Task<string> HandleAsync(Fruit payload, CancellationToken cancellationToken = default)
    {
        await pearJuiceHandler.HandleAsync(payload, cancellationToken);
        return $"Juice of Pear: {(payload as Pear)?.PearName ?? payload.GetType().Name}";
    }
}