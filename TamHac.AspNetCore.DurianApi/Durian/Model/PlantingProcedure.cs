namespace TamHac.AspNetCore.DurianApi.Durian.Model;

public class PlantingProcedure
{
    public string? Climate { get; init; }
    public string? PlantingArea { get; init; }
    public string? PlantingDensity { get; set; }
    public string? PlantingStandards { get; set; }
    public string? PlantingTechniques { get; set; }
    public string? GrowingTime { get; init; }
    public string? LifeSpan { get; init; }
}