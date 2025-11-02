using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using SQLitePCL;

namespace Examen1PMUCENM2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Batteries_V2.Init();
            SQLitePCL.raw.SetProvider(new SQLite3Provider_dynamic_cdecl());

            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>() // 👈 Este debe ir primero
                .UseMauiCommunityToolkitMediaElement() // 👈 Este debe ir justo después si lo usas
                .UseMauiCommunityToolkit() // 👈 Este puede ir después
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