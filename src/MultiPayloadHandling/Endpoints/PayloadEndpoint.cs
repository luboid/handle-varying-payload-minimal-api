namespace MultiPayloadHandling.Endpoints;

public static class FruitHandlerEndpoint
{
    public static void AddFruitHandlerEndpoint(this WebApplication app)
    {
        app.MapPost("/FruitHandler", FruitHandler)
        .WithName("FruitHandler")
        .WithOpenApi()
        .Accepts<FruitPayload>("application/json");
    }

    public static async Task<IResult> FruitHandler(
        FruitPayload payload, 
        [FromServices] IFruitHandlerFactory fruitHandlerFactory, 
        CancellationToken cancellationToken)
    {
        var handler = fruitHandlerFactory.Create(payload.FruitType);
        var juiceDesc = await handler.HandleAsync(payload, cancellationToken);
        return Results.Ok(new { Juice = juiceDesc });
    }
}
