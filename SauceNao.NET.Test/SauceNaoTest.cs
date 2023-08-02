using RichardSzalay.MockHttp;
using SauceNao.NET.Data;

namespace SauceNao.NET.Test;

public class SauceNaoTest {
    private MockHttpMessageHandler MockHttp { get; } = new();
    private byte[] TestImage { get; } = File.ReadAllBytes("TestData/test_image.png");

    [Fact]
    public async Task InvalidApiKey() {
        SauceNao sauceNao = new("invalid", MockHttp.ToHttpClient());

        string response = await File.ReadAllTextAsync("TestData/invalid_key.json");

        MockHttp.When(SauceNao.BaseUrl).Respond("application/json", response);
        SearchResult result = await sauceNao.Search(TestImage);
        Assert.NotNull(result);
    }
}