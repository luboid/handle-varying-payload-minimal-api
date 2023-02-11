namespace MultiPayloadHandling.Interfaces;

public interface IAppleJuiceHandler
{
    Task HandleAsync(Payload payload, CancellationToken cancellationToken = default);
}