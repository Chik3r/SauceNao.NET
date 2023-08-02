using RichardSzalay.MockHttp;
using SauceNao.NET.Model;
using SauceNao.NET.Exceptions;

namespace SauceNao.NET.Test;

public class SauceNaoTest {
    private MockHttpMessageHandler MockHttp { get; } = new();
    private byte[] TestImage { get; } = File.ReadAllBytes("TestData/test_image.png");

    [Fact]
    public async Task InvalidApiKey() {
        SauceNao sauceNao = new("invalid", MockHttp.ToHttpClient());

        string response = await File.ReadAllTextAsync("TestData/invalid_key.json");

        MockHttp.When(SauceNao.BaseUrl).Respond("application/json", response);
        await Assert.ThrowsAsync<UnknownClientError>(async () => await sauceNao.Search(TestImage));
    }

    [Fact]
    public async Task RequestImage() {
        SauceNao sauceNao = new("valid", MockHttp.ToHttpClient());
        
        string response = await File.ReadAllTextAsync("TestData/valid_response.json");
        
        MockHttp.When(SauceNao.BaseUrl)
            .WithQueryString("api_key", "valid")
            .Respond("application/json", response);
        SearchResult result = await sauceNao.Search(TestImage);
        Assert.NotNull(result.Results);
        Assert.NotEmpty(result.Results);

        Data first = result.Results[0].Data;
        Assert.Equal(51318798, first.PixivId);
    }

    [Fact]
    public async Task RequestWebImage() {
        SauceNao sauceNao = new("valid", MockHttp.ToHttpClient());
        
        string response = await File.ReadAllTextAsync("TestData/url_response.json");
        
        MockHttp.When(SauceNao.BaseUrl)
            .WithQueryString("api_key", "valid")
            .WithQueryString("url", "https://wikipedia.org/static/images/icons/wikipedia.png")
            .Respond("application/json", response);
        SearchResult result = await sauceNao.Search("https://wikipedia.org/static/images/icons/wikipedia.png");
        Assert.NotNull(result.Results);
        Assert.NotEmpty(result.Results);

        Data first = result.Results[0].Data;
        Assert.Equal("925729264", first.DaId);
    }

    [Fact]
    public async Task RequestInvalidWebImage() {
        SauceNao sauceNao = new("valid", MockHttp.ToHttpClient());
        
        string response = await File.ReadAllTextAsync("TestData/invalid_url_response.json");
        
        MockHttp.When(SauceNao.BaseUrl)
            .WithQueryString("api_key", "valid")
            .WithQueryString("url", "https://wikipedia.org/static/images/icons/wikipedia.pn")
            .Respond("application/json", response);
        await Assert.ThrowsAsync<UnknownClientError>(async () => await sauceNao.Search("https://wikipedia.org/static/images/icons/wikipedia.pn"));
    }
}