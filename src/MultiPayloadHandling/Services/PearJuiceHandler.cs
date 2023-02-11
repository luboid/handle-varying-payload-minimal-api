namespace MultiPayloadHandling.Services;

public class PearJuiceHandler : IPearJuiceHandler
{
    public Task HandleAsync(Payload payload, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}