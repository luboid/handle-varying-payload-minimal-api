namespace MultiPayloadHandling.Endpoints;


public static class WetherForecastEndpoint
{
    private static string[] summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public static void AddWetherForecastEndpoint(this WebApplication app)
    {
        app.MapGet("/GetWeatherForecast", Handler)
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }

    public static Task<WeatherForecast[]> Handler(HttpContext httpContext, CancellationToken cancellationToken)
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            })
            .ToArray();

        return Task.FromResult( forecast);
    }
}
