using MultiPayloadHandling.Attributes;

namespace MultiPayloadHandling.Endpoints;

public static class FruitHandlerEndpoint
{
    public static void AddFruitHandlerEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/FruitHandler", FruitHandler)
        .WithName("FruitHandler")
        .WithOpenApi()
        .Accepts<Fruit>("application/json");
    }

    public static async Task<IResult> FruitHandler(
        [FromBody, Validate] Fruit fruit, 
        [FromServices] IFruitHandlerFactory fruitHandlerFactory,
        CancellationToken cancellationToken)
    {
        var handler = fruitHandlerFactory.Create(fruit.Type);
        var juiceDesc = await handler.HandleAsync(fruit, cancellationToken);
        return Results.Ok(new { Juice = juiceDesc });
    }
}
