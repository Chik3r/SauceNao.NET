using System.Text.Json.Serialization;

namespace SauceNao.NET.Data; 

public record ResultHeader(
    [property: JsonPropertyName("user_id")] string UserId,
    [property: JsonPropertyName("account_type")] string AccountType,
    [property: JsonPropertyName("short_limit")] string ShortLimit,
    [property: JsonPropertyName("long_limit")] string LongLimit,
    [property: JsonPropertyName("long_remaining")] int LongRemaining,
    [property: JsonPropertyName("short_remaining")] int ShortRemaining,
    [property: JsonPropertyName("status")] int Status,
    [property: JsonPropertyName("message")] string? Message,
    [property: JsonPropertyName("results_requested")] int ResultsRequested,
    [property: JsonPropertyName("index")] Dictionary<string, Index> Indexes,
    [property: JsonPropertyName("search_depth")] string SearchDepth,
    [property: JsonPropertyName("minimum_similarity")] double MinimumSimilarity,
    [property: JsonPropertyName("query_image_display")] string QueryImageDisplay,
    [property: JsonPropertyName("query_image")] string QueryImage,
    [property: JsonPropertyName("results_returned")] int ResultsReturned
);