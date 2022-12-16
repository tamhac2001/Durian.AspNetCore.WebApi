namespace TamHac.AspNetCore.DurianApi.configuration;

public class DurianDatabaseConfiguration
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string DuriansCollectionName { get; set; } = null!;
}