using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TamHac.AspNetCore.DurianApi.Domain;

public record Durian
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; } = default;

    public string? Code { get; init; }

    public string? Name { get; init; }

    public string? Origin { get; init; }

    public Characteristics Characteristics { get; init; }

    public PlantingProcedure PlantingProcedure { get; init; }

    public string? Productivity { get; init; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal? Price { get; init; }

    public IEnumerable<string>? ImageUrls { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
}