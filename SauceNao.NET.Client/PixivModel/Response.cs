using System.Text.Json.Serialization;

namespace SauceNao.NET.Client.PixivModel; 

public record Root(
    [property: JsonPropertyName("error")] bool Error,
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("body")] Illustration Body
);