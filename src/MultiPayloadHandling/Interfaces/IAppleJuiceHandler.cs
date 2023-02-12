namespace MultiPayloadHandling.Interfaces;

public interface IAppleJuiceHandler
{
    Task HandleAsync(Fruit payload, CancellationToken cancellationToken = default);
}