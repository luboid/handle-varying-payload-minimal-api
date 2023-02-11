namespace MultiPayloadHandling.Interfaces;

public interface IFruitHandler
{
    Task<string> HandleAsync(FruitPayload payload, CancellationToken cancellationToken = default);
}