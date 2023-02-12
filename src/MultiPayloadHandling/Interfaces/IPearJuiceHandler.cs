namespace MultiPayloadHandling.Interfaces;

public interface IPearJuiceHandler
{
    Task HandleAsync(Fruit payload, CancellationToken cancellationToken = default);
}