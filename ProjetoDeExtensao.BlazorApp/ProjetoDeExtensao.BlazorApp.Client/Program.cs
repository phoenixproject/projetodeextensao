using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjetoDeExtensao.BlazorApp.Client.Pages;

namespace ProjetoDeExtensao.BlazorApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

			// Para Blazor WebAssembly
			//builder.Services.AddScoped<Login>();

			builder.Services.AddScoped(sp => new HttpClient
			{
				BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
			});

			builder.Services.AddScoped(sp =>
			{
				var config = sp.GetRequiredService<IConfiguration>();
				var apiUrl = config["ApiUrl"] ?? builder.HostEnvironment.BaseAddress;
				Console.WriteLine($"API URL: {apiUrl}");
				return new HttpClient { BaseAddress = new Uri(apiUrl) };
			});

			await builder.Build().RunAsync();
        }
    }
}
