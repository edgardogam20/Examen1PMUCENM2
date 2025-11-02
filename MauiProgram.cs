using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using SQLitePCL;

namespace Examen1PMUCENM2
{
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            SQLitePCL.Batteries.Init();
            SQLitePCL.Batteries_V2.Init();
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}