namespace MultiPayloadHandling.Services;

[FruitHandler(FruitType.Pear)]
public class PearHandler : IPearHandler
{
    private readonly IPearJuiceHandler pearJuiceHandler;

    public PearHandler(IPearJuiceHandler pearJuiceHandler)
    {
        this.pearJuiceHandler = pearJuiceHandler;
    }

    public async Task<string> HandleAsync(FruitPayload payload, CancellationToken cancellationToken = default)
    {
        await pearJuiceHandler.HandleAsync(payload, cancellationToken);
        return $"Juice of {(payload as PearPayload)?.PearName ?? payload.GetType().Name}";
    }
}