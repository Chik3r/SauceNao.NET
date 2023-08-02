using System.Text.Json.Serialization;

namespace SauceNao.NET.Model; 

public record SearchResult(
    [property: JsonPropertyName("header")] ResultHeader Header,
    [property: JsonPropertyName("results")] List<Result>? Results
);