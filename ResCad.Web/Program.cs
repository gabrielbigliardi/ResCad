using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ResCad.Web;
using ResCad.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure o HttpClient com o nome "API"
//builder.Services.AddHttpClient("API", client => {
//    client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//});

//builder.Services.AddScoped(sp =>
//{
//    var httpClient = new HttpClient
//    {
//        BaseAddress = new Uri("https://localhost:7287/") // URL DIRETA da API
//    };
//    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
//    return httpClient;
//});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7287/")
});

builder.Services.AddMudServices();

// Registre o serviço ResidentesAPI
builder.Services.AddTransient<ResidentesAPI>();

await builder.Build().RunAsync();