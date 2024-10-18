using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaRefeicao.Utils;
using ReservaRefeicao.Services;

namespace ReservaRefeicao
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


            // Configurar o DbContext para SQL Server
            builder.Services.AddDbContext<DbContextServices>(options =>
            {
                options.UseSqlServer(Configuracao.ObterInstancia().ObterConnectionString("SistemaTramontina").ConnectionString);
            });

//#if DEBUG
//            builder.Logging.AddDebug();
//#endif

            return builder.Build();
        }
    }
}
