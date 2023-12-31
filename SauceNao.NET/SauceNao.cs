﻿using System.Collections.Specialized;
using System.Text.Json;
using System.Web;
using SauceNao.NET.Model;
using SauceNao.NET.Exceptions;

namespace SauceNao.NET;

public class SauceNao {
    public const string BaseUrl = "https://saucenao.com/search.php";
    
    private string ApiKey { get; }
    private HttpClient Client { get; }

    public SauceNao(string apiKey, HttpClient? client = null) {
        ApiKey = apiKey;
        Client = client ?? new HttpClient();
    }

    public async Task<SearchResult> Search(byte[] image) {
        Dictionary<string, string> parameters = new() {
            ["api_key"] = ApiKey,
            ["output_type"] = ((int)OutputType.Json).ToString(),
            // ["dbmask"]
            // ["dbmaski"]
            // ["db"]
            // ["dbs[]"]
            // ["numres"]
            // ["dedupe"]
            // ["hide"]
        };
        Uri uri = AddParameters(new Uri(BaseUrl), parameters);

        using MemoryStream stream = new(image);
        MultipartFormDataContent content = new();
        content.Add(new StreamContent(stream), "file", "image.png");

        HttpResponseMessage response = await Client.PostAsync(uri, content);
        response.EnsureSuccessStatusCode();
        
        return await ParseResponse(response);
    }
    
    public async Task<SearchResult> Search(string imageUrl) {
        Dictionary<string, string> parameters = new() {
            ["api_key"] = ApiKey,
            ["output_type"] = OutputType.Json.ToString(),
            ["url"] = imageUrl,
            // ["dbmask"]
            // ["dbmaski"]
            // ["db"]
            // ["dbs[]"]
            // ["numres"]
            // ["dedupe"]
            // ["hide"]
        };
        Uri uri = AddParameters(new Uri(BaseUrl), parameters);

        HttpResponseMessage response = await Client.PostAsync(uri, null);
        response.EnsureSuccessStatusCode();
        
        return await ParseResponse(response);
    }

    private static async Task<SearchResult> ParseResponse(HttpResponseMessage response) {
        string json = await response.Content.ReadAsStringAsync();
        SearchResult result = JsonSerializer.Deserialize<SearchResult>(json) ?? throw new NullReferenceException();

        if (result.Header.Status < 0)
            throw new UnknownClientError();
        if (result.Header.Status > 0)
            throw new UnknownServerError();
        if (result.Header.ShortRemaining < 0)
            throw new RateLimitException(RateLimitReached.ShortLimit);
        if (result.Header.LongRemaining < 0)
            throw new RateLimitException(RateLimitReached.LongLimit);

        return result;
    }

    private static Uri AddParameters(Uri uri, Dictionary<string, string> parameters) {
        UriBuilder builder = new(uri);
        NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
        foreach ((string key, string value) in parameters) 
            query.Add(key, value);
        builder.Query = query.ToString();
        return builder.Uri;
    }
}