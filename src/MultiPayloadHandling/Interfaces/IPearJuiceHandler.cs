namespace MultiPayloadHandling.Interfaces;

public interface IPearJuiceHandler
{
    Task HandleAsync(Payload payload, CancellationToken cancellationToken = default);
}