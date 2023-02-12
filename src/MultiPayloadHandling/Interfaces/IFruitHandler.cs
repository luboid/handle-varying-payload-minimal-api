namespace MultiPayloadHandling.Interfaces;

public interface IFruitHandler
{
    Task<string> HandleAsync(Fruit payload, CancellationToken cancellationToken = default);
}