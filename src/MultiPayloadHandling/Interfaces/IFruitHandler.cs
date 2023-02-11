namespace MultiPayloadHandling.Interfaces;

public interface IFruitHandler
{
    Task<string> HandleAsync(Payload payload, CancellationToken cancellationToken = default);
}