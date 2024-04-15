using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
// ReSharper disable StringLiteralTypo

namespace MicroGauge.Example.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("verdana.ttf", "VerdanaRegular");
                    fonts.AddFont("verdanab.ttf", "VerdanaBold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
