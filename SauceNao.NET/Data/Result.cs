using System.Text.Json.Serialization;

namespace SauceNao.NET.Data; 

public record Result(
    [property: JsonPropertyName("header")] DataHeader Header,
    [property: JsonPropertyName("data")] Data Data
);