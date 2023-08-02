using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
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

    public async ValueTask ExecuteAsync(IConsole console) {
        if (!File.Exists(FilePath))
            throw new FileNotFoundException("The specified file does not exist.", FilePath);
        
        SauceNao sauceNao = new(ApiKey);
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
    }
}