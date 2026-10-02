using ClientApp;
using ClientApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

builder.Services.AddScoped<ProductApiClient>();

await builder.Build().RunAsync();
