using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapPost("/upload", async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();
    var file = form.Files["file"];

    if (file == null || file.Length == 0)
    {
        return Results.BadRequest("No file uploaded.");
    }

    var random = new Random();
    var classifications = new[]
    {
            new ClassificationResponseDto { Classification = "BrokenDoorRepair", RiskLevel = "High" },
            new ClassificationResponseDto { Classification = "RoofingTileReplacement", RiskLevel = "High" },
            new ClassificationResponseDto { Classification = "WaterLeakDetection", RiskLevel = "Low" },
            new ClassificationResponseDto { Classification = "BasementWaterproofing", RiskLevel = "Low" },
            new ClassificationResponseDto { Classification = "FireDamagedWallRepair", RiskLevel = "High" },
            new ClassificationResponseDto { Classification = "BrokenFloorTileRepair", RiskLevel = "High" }
        };

    var response = classifications[random.Next(classifications.Length)];
    return Results.Ok(response);
});

app.Run();

public class ClassificationResponseDto
{
    public string Classification { get; set; }
    public string RiskLevel { get; set; }
}
