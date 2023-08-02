using System.Collections.Specialized;
using System.Text.Json;
using System.Web;
using SauceNao.NET.Data;

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
            ["output_type"] = OutputType.Json.ToString(),
            // ["dbmask"]
            // ["dbmaski"]
            // ["db"]
            // ["dbs[]"]
            // ["numres"]
            // ["dedupe"]
            // ["hide"]
        };
        Uri uri = AddParameters(new Uri(BaseUrl), parameters);

        MultipartContent content = new();
        content.Add(new ByteArrayContent(image));

        HttpResponseMessage response = await Client.PostAsync(uri, content);
        response.EnsureSuccessStatusCode();
        
        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SearchResult>(json);
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