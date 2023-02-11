namespace MultiPayloadHandling.Models;

public class ApplePayload : Payload
{
    public ApplePayload()
    {
        FruitType = FruitType.Apple;
    }

    public int AppleAttribute { get; set; }

    public string? AppleName { get; set; }
}