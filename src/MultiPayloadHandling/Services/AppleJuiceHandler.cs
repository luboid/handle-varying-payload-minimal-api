namespace MultiPayloadHandling.Services;

public class AppleJuiceHandler : IAppleJuiceHandler
{
    public Task HandleAsync(Payload payload, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}