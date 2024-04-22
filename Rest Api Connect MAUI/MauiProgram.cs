using MAUI.WebApi.Interface;
using Microsoft.Extensions.Logging;

namespace Rest_Api_Connect_MAUI
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<IPlatforumHttpMessageHandler>(_ =>
                {
#if ANDROID
                    return new Platforms.Android.AndroidHttpMessageHandler();
#elif IOS
                    return new Platforms.iOS.IosHttpMessageHandler();
#endif
                });
            builder.Services.AddHttpClient("maui-to-https-localhost", httpClient =>
            {
                var bseAddress = DeviceInfo.Platform == DevicePlatform.Android
                ? "https://10.0.2.2:12345"
                : "https://localhost:12345";
                httpClient.BaseAddress = new Uri(bseAddress);
            })
                .ConfigureHttpMessageHandlerBuilder(httpClient =>
                {
                    var platforumHttpMessageHandler = httpClient.Services
                    .GetRequiredService<IPlatforumHttpMessageHandler>();
                    httpClient.PrimaryHandler = platforumHttpMessageHandler.GetHttpMessageHandler();
                });
            // builder.Services.AddHttpClient("api", httpClient=>httpClient.BaseAddress=new Uri("https://localhost:7063/WeatherForecast"));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
