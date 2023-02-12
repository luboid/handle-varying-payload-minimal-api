namespace MultiPayloadHandling.Models;

// Net 7 way with default converters
// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/polymorphism?pivots=dotnet-7-0
// [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
// [JsonDerivedType(typeof(Apple), nameof(FruitType.Apple))]
// [JsonDerivedType(typeof(Pear), nameof(FruitType.Pear))]
public class Fruit
{
    public virtual FruitType Type { get; }

    // thru the binding system
    // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/parameter-binding?view=aspnetcore-7.0#bindasync
    // public static async ValueTask<Fruit?> BindAsync(HttpContext context, ParameterInfo parameter)
    // {
    //     using var json = await JsonDocument.ParseAsync(context.Request.Body, default, context.RequestAborted);
    //     if (Enum.TryParse<FruitType>(json.RootElement.GetProperty("type").ToString(), true, out var fruitType))
    //     {
    //         var type = fruitType switch
    //         {
    //             FruitType.Pear => typeof(Pear),
    //             FruitType.Apple => typeof(Apple),
    //             _ => throw new ArgumentOutOfRangeException(nameof(fruitType)),
    //         };
    // 
    //         var jsonOptions = context.RequestServices.GetRequiredService<IOptionsSnapshot<JsonOptions>>().Value;
    // 
    //         var payload = json.Deserialize(type, jsonOptions.JsonSerializerOptions) as Fruit;
    // 
    //         return payload;
    //     }
    //     else
    //     {
    //         throw new ArgumentOutOfRangeException(nameof(fruitType));
    //     }
    // }
}