using CliFx;

namespace SauceNao.NET.Client; 

class Program {
    public static async Task<int> Main() =>
        await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .Build()
            .RunAsync();
}