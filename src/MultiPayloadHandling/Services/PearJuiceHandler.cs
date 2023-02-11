namespace MultiPayloadHandling.Services;

public class PearJuiceHandler : IPearJuiceHandler
{
    public Task HandleAsync(FruitPayload payload, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}