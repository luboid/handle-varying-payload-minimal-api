namespace MultiPayloadHandling.Services;

public class AppleJuiceHandler : IAppleJuiceHandler
{
    public Task HandleAsync(FruitPayload payload, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}