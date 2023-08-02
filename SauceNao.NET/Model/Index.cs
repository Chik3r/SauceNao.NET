using System.Text.Json.Serialization;

namespace SauceNao.NET.Model; 

public record Index(
    [property: JsonPropertyName("status")] int Status,
    [property: JsonPropertyName("parent_id")] int ParentId,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("results")] int Results
);