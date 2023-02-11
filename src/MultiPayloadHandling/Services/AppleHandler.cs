namespace MultiPayloadHandling.Services;

[FruitSrevice(FruitType.Apple)]
public class AppleHandler : IAppleHandler
{
    private readonly IAppleJuiceHandler appleJuiceHandler;

    public AppleHandler(IAppleJuiceHandler appleJuiceHandler)
    {
        this.appleJuiceHandler = appleJuiceHandler;
    }

    public async Task<string> HandleAsync(Payload payload, CancellationToken cancellationToken = default)
    {
        await appleJuiceHandler.HandleAsync(payload, cancellationToken);
        return $"Juice of {(payload as ApplePayload)?.AppleName ?? payload.GetType().Name}";
    }
}