using Dapr.Client;
using Dapr.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();

//var daprClient = new DaprClientBuilder().Build();
//builder.Configuration.AddDaprSecretStore("localsecretstore", daprClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async ([FromServices] ILogger<Program> logger, [FromServices] IConfiguration configuration) =>
{
    var daprClient = new DaprClientBuilder().Build();
    var secrets = await daprClient.GetSecretAsync("localsecretstore", "redisPassword");
    var secrets2 = await daprClient.GetSecretAsync("localsecretstore", "connectionStrings");
    var data22 = secrets2["mysql:username"];

    //var asdasd = configuration["connectionStrings:mysql:username"];

    await Task.Delay(TimeSpan.FromSeconds(2));

    logger.LogInformation("Generando datos weatherforecast " + data22);

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            data22
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
