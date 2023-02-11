namespace MultiPayloadHandling.Interfaces;

public interface IAppleJuiceHandler
{
    Task HandleAsync(FruitPayload payload, CancellationToken cancellationToken = default);
}