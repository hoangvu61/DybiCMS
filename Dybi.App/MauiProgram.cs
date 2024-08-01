using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Dybi.App.Services;
using Blazored.Toast;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.LifecycleEvents;

namespace Dybi.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            // Make sure to add "using Microsoft.Maui.LifecycleEvents;" in the top of the file 
            events.AddWindows(windowsLifecycleBuilder =>
            {
                windowsLifecycleBuilder.OnWindowCreated(window =>
                {
                    window.ExtendsContentIntoTitleBar = false;
                    var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                    var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
                    switch (appWindow.Presenter)
                    {
                        case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
                            //overlappedPresenter.SetBorderAndTitleBar(false, false);
                            overlappedPresenter.Maximize();
                            break;
                    }
                });
            });
        });
#endif

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            
            builder.Services.AddSingleton<GlobalVariables>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddTransient<IOrderApiClient, OrderApiClient>();
            builder.Services.AddTransient<IWarehouseApiClient, WarehouseApiClient>();
            builder.Services.AddTransient<IWebsiteApiClient, WebsiteApiClient>();
            builder.Services.AddTransient<IFileService, FileService>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredToast();

            builder.Configuration.AddJsonFile("appsettings.json");

            builder.Services.AddScoped(sp => new ApiCaller
            {
                httpClient = new HttpClient { BaseAddress = new Uri(builder.Configuration["BackendApiUrl"]) },
                UseHttpMethodOverride = bool.Parse(builder.Configuration["UseHttpMethodOverride"])
            });

            return builder.Build();
        }
    }
}
