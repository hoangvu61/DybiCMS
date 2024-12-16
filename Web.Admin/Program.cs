using Blazored.LocalStorage;
using Blazored.Toast;
using Dybi.Library;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.Admin;
using Web.Admin.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddTransient<IUserApiClient, UserApiClient>();
builder.Services.AddTransient<ICompanyApiClient, CompanyApiClient>(); 
builder.Services.AddTransient<IModuleApiClient, ModuleApiClient>();
builder.Services.AddTransient<ITemplateApiClient, TemplateApiClient>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IThirdPartyApiClient, ThirdPartyApiClient>();
builder.Services.AddTransient<IModuleConfigApiClient, ModuleConfigApiClient>();
builder.Services.AddTransient<IContentApiClient, ContentApiClient>();
builder.Services.AddTransient<IAttributeApiClient, AttributeApiClient>();
builder.Services.AddTransient<IOrderApiClient, OrderApiClient>();
builder.Services.AddTransient<IMenuApiClient, MenuApiClient>();
builder.Services.AddTransient<ISEOApiClient, SEOApiClient>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped(sp => new ApiCaller
{
    httpClient = new HttpClient { BaseAddress = new Uri(builder.Configuration["BackendApiUrl"]) },
    UseHttpMethodOverride = bool.Parse(builder.Configuration["UseHttpMethodOverride"])
});

await builder.Build().RunAsync();
