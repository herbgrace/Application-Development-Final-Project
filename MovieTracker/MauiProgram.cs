using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using MovieTracker.Services;
using System.Net.Http;
using System.Reflection;

namespace MovieTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            DotNetEnv.Env.Load("auth.env");

            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddSingleton<TmdbService>();
            builder.Services.AddHttpClient();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }}