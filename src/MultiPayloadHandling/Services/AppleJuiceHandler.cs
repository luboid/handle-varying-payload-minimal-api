namespace MultiPayloadHandling.Services;

public class AppleJuiceHandler : IAppleJuiceHandler
{
    public Task HandleAsync(Fruit payload, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}