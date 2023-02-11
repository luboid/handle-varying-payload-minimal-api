namespace MultiPayloadHandling.Models;

public class PearPayload : Payload
{
    public PearPayload()
    {
        FruitType = FruitType.Pear;
    }

    public int PearAttribute { get; set; }

    public string? PearName { get; set; }
}