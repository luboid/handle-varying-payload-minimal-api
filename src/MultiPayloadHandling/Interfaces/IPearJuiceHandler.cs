namespace MultiPayloadHandling.Interfaces;

public interface IPearJuiceHandler
{
    Task HandleAsync(FruitPayload payload, CancellationToken cancellationToken = default);
}