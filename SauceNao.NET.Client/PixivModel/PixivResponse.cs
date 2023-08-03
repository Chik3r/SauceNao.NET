using System.Text.Json.Serialization;

namespace SauceNao.NET.Client.PixivModel; 

public record PixivResponse(
    [property: JsonPropertyName("error")] bool Error,
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("body")] Illustration Body
);