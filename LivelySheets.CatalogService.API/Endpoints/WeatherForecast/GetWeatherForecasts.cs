using LivelySheets.CatalogService.API.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace LivelySheets.CatalogService.API.Endpoints.WeatherForecast;

public class GetWeatherForecasts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", async ([FromServices] MatchupServiceClient httpClient) =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();

            //temporary dummy test of linking with the MatchupService.API
            var result = await httpClient.SendOutboxMessageAsync();

            return forecast.Prepend(new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now),
                Random.Shared.Next(-20, 55),
                result)
            );

        })
        .RequireAuthorization()
        .WithName("GetWeatherForecast")
        .Produces<WeatherForecast[]>()
        .Produces(StatusCodes.Status401Unauthorized)
        .WithOpenApi();
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
