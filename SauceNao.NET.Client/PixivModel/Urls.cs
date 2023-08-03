using System.Text.Json.Serialization;

namespace SauceNao.NET.Client.PixivModel; 

public record Urls(
    [property: JsonPropertyName("mini")] string Mini,
    [property: JsonPropertyName("thumb")] string Thumb,
    [property: JsonPropertyName("small")] string Small,
    [property: JsonPropertyName("regular")] string Regular,
    [property: JsonPropertyName("original")] string Original
);