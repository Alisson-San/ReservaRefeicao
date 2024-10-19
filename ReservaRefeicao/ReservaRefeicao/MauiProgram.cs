using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaRefeicao.Utils;
using ReservaRefeicao.Services;
using ReservaRefeicao.Model;
using ReservaRefeicao.ModelView;
using ReservaRefeicao.Views;

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

            builder.Services.AddTransient<GestorSessaoService>();

            // Registra a SessaoUsuario
            builder.Services.AddSingleton<Sessao>();

            // Registra o AuthenticationViewModel
            builder.Services.AddTransient<AuthenticationViewModel>();

            builder.Services.AddTransient<AuthenticationView>();

            //#if DEBUG
            //            builder.Logging.AddDebug();
            //#endif

            return builder.Build();
        }
    }
}
