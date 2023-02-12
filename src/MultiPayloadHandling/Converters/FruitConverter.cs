namespace MultiPayloadHandling.Converters;

public class FruitConverter : JsonConverter<Fruit>
{
    public override Fruit? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);

        if (Enum.TryParse<FruitType>(json.RootElement.GetProperty("type").ToString(), true, out var fruitType))
        {
            var type = fruitType switch
            {
                FruitType.Pear => typeof(Pear),
                FruitType.Apple => typeof(Apple),
                _ => throw new ArgumentOutOfRangeException(nameof(fruitType)),
            };

            var fruit = json.Deserialize(type, options) as Fruit;

            return fruit;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(fruitType));
        }
    }

    public override void Write(Utf8JsonWriter writer, Fruit value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.GetType(), options);
    }
}