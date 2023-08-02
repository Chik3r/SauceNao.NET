using System.Text.Json.Serialization;

namespace SauceNao.NET.Data; 

public record DataHeader(
    [property: JsonPropertyName("similarity")] string Similarity,
    [property: JsonPropertyName("thumbnail")] string Thumbnail,
    [property: JsonPropertyName("index_id")] int IndexId,
    [property: JsonPropertyName("index_name")] string IndexName,
    [property: JsonPropertyName("dupes")] int Dupes,
    [property: JsonPropertyName("hidden")] int Hidden
);