using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace MAUI
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            DatePickerHandler.Mapper.AppendToMapping("CustomBorder", (handler, view) =>
            {
#if WINDOWS
                // En Windows, es el más directo
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(2, 0, 0, 0);
                handler.PlatformView.BorderBrush = Microsoft.Maui.Graphics.Colors.Blue.ToPlatform();
#elif ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.Background = null;
#endif
            });


            return builder.Build();
        }
    }
}
