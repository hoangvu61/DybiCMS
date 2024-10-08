using Blazored.LocalStorage;
using Blazored.Toast;
using Library;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Web.App;
using Web.App.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<GlobalVariables>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddTransient<IOrderApiClient, OrderApiClient>();
builder.Services.AddTransient<IWarehouseApiClient, WarehouseApiClient>();
builder.Services.AddTransient<IWebsiteApiClient, WebsiteApiClient>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IDebtApiClient, DebtApiClient>();
builder.Services.AddTransient<IAttributeApiClient, AttributeApiClient>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredToast();
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new ApiCaller
{
    httpClient = new HttpClient { BaseAddress = new Uri(builder.Configuration["BackendApiUrl"]) },
    UseHttpMethodOverride = bool.Parse(builder.Configuration["UseHttpMethodOverride"])
});

await builder.Build().RunAsync();
