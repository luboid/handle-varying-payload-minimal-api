namespace MultiPayloadHandling.Models;

public class FruitPayload
{
    public FruitType FruitType { get; set; }

    public static async ValueTask<FruitPayload?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        using var json = await JsonDocument.ParseAsync(context.Request.Body, default, context.RequestAborted);
        if (Enum.TryParse<FruitType>(json.RootElement.GetProperty("fruitType").ToString(), true, out var fruitType))
        {
            var type = fruitType switch
            {
                FruitType.Pear => typeof(PearPayload),
                FruitType.Apple => typeof(ApplePayload),
                _ => throw new ArgumentOutOfRangeException(nameof(fruitType)),
            };

            var jsonOptions = context.RequestServices.GetRequiredService<IOptionsSnapshot<JsonOptions>>().Value;

            var payload = json.Deserialize(type, jsonOptions.JsonSerializerOptions) as FruitPayload;

            return payload;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(fruitType));
        }
    }
}