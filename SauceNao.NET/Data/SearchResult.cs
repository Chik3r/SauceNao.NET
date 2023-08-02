using System.Text.Json.Serialization;

namespace SauceNao.NET.Data; 

public record SearchResult(
    [property: JsonPropertyName("header")] ResultHeader Header,
    [property: JsonPropertyName("data")] Data? Data
);