namespace MultiPayloadHandling.Services;

public class PearJuiceHandler : IPearJuiceHandler
{
    public Task HandleAsync(Fruit payload, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}