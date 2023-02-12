using MultiPayloadHandling.Attributes;

namespace MultiPayloadHandling.Endpoints;

public static class FruitHandlerEndpoint
{
    public static void AddFruitHandlerEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/FruitHandler", FruitHandler)
        .WithName("FruitHandlerCreate")
        .WithOpenApi()
        .Accepts<Fruit>("application/json");
        group.MapGet("/FruitHandler", FruitHandlerGet)
        .WithName("FruitHandlerGet")
        .WithOpenApi();
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

    public static Task<IResult> FruitHandlerGet(CancellationToken cancellationToken)
    {
        return Task.FromResult(Results.Ok(new Fruit[] 
        { 
            new Apple { AppleName = "The Apple" }, 
            new Pear { PearName = "The Pear" } 
        }));
    }
}
