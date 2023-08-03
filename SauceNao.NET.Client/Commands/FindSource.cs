using System.Text.Json;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using SauceNao.NET.Client.PixivModel;
using SauceNao.NET.Model;

namespace SauceNao.NET.Client.Commands; 

[Command(Description = "Finds the source of an image.")]
public class FindSource : ICommand {
    [CommandParameter(1, Description = "The path to the image file.")]
    public required string FilePath { get; init; }
    
    [CommandParameter(0, Description = "SauceNao API key.")]
    public required string ApiKey { get; init; }

    [CommandOption("similarity", 's', Description = "Minimum similarity to use when searching.")]
    public double MinimumSimilarity { get; set; } = 80.0;

    [CommandOption("download", 'd', Description = "Downloads the image from pixiv (if found)")]
    public bool Download { get; set; }

    public async ValueTask ExecuteAsync(IConsole console) {
        if (!File.Exists(FilePath))
            throw new FileNotFoundException("The specified file does not exist.", FilePath);

        HttpClient client = new();
        SauceNao sauceNao = new(ApiKey, client);
        SearchResult searchResult = await sauceNao.Search(await File.ReadAllBytesAsync(FilePath));
        List<Result>? validResults = searchResult.Results?.Where(x => {
            if (!double.TryParse(x.Header.Similarity, out double similarity))
                return false;
            return similarity > MinimumSimilarity;
        }).ToList();

        if (validResults is null || validResults.Count == 0) {
            await console.Output.WriteLineAsync("No results found.");
            return;
        }

        await console.Output.WriteLineAsync($"{"Similarity",-15} {"URL",3}");
        foreach (Result result in validResults) {
            string url = result.Data.ExtUrls?[0] ?? "No URL found.";
            await console.Output.WriteLineAsync($"{result.Header.Similarity,-15} {url,3}");
        }
        
        if (!Download)
            return;

        Data data = validResults[0].Data;
        if (data.PixivId is null) {
            await console.Output.WriteLineAsync("Highest similarity result does not have a pixiv ID.");
            return;
        }

        await console.Output.WriteLineAsync("Requesting image info...");
        HttpRequestMessage requestMessage = new(HttpMethod.Get, $"https://www.pixiv.net/ajax/illust/{data.PixivId}");
        HttpResponseMessage pixivResult = await client.SendAsync(requestMessage);
        pixivResult.EnsureSuccessStatusCode();
        
        string pixivResponse = await pixivResult.Content.ReadAsStringAsync();
        PixivResponse? pixiv = JsonSerializer.Deserialize<PixivResponse>(pixivResponse);

        if (pixiv is null || pixiv.Error) {
            await console.Output.WriteLineAsync("Failed to deserialize pixiv response");
            return;
        }

        await console.Output.WriteLineAsync("Downloading...");
        string pixivUrl = pixiv.Body.Urls!.Original;
        await DownloadFile(client, pixivUrl, FilePath);
    }
    
    private static async Task DownloadFile(HttpClient client, string url, string originalFilename) {
        HttpRequestMessage requestMessage = new(HttpMethod.Get, url);
        requestMessage.Headers.Referrer = new Uri("https://www.pixiv.net/");
        HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
        responseMessage.EnsureSuccessStatusCode();
        byte[] byteArray = await responseMessage.Content.ReadAsByteArrayAsync();

        Uri uri = new(url);
        string extension = Path.GetExtension(uri.LocalPath);

        string fileName = Path.Join(Path.GetDirectoryName(originalFilename),
            Path.GetFileNameWithoutExtension(originalFilename) + "_pixiv" + extension);

        Image image = Image.Load(byteArray);
        await image.SaveAsync(fileName);
    }
}