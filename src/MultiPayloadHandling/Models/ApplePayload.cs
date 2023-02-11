namespace MultiPayloadHandling.Models;

public class ApplePayload : FruitPayload
{
    public ApplePayload()
    {
        FruitType = FruitType.Apple;
    }

    public int AppleAttribute { get; set; }

    public string? AppleName { get; set; }
}