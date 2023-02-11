namespace MultiPayloadHandling.Endpoints;

public static class PayloadEndpoint
{
    public static void AddPayloadEndpoint(this WebApplication app)
    {
        app.MapPost("/CreatePayload", Handler)
        .WithName("CreatePayload")
        .WithOpenApi()
        .Accepts<Payload>("application/json");
    }

    public static async Task<IResult> Handler(
        Payload payload, 
        [FromServices] IFruitHandlerFactory fruitHandlerFactory, 
        CancellationToken cancellationToken)
    {
        var handler = fruitHandlerFactory.Create(payload.FruitType);
        var juiceDesc = await handler.HandleAsync(payload, cancellationToken);
        return Results.Ok(new { Juice = juiceDesc });
    }
}
