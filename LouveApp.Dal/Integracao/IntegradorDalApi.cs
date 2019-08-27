using LouveApp.Compartilhado.Transacoes;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Servicos;
using LouveApp.Dal.Contexto;
using Microsoft.Extensions.DependencyInjection;
using LouveApp.Dal.Transacoes;
using LouveApp.Dal.Repositorios;
using LouveApp.Dominio.Sistema;
using Microsoft.EntityFrameworkCore;

namespace LouveApp.Dal.Integracao
{
    public static class IntegradorDalApi
    {
        public static void AdicionarBancoContexto(this IServiceCollection services)
        {
            if (Configuracoes.EmDesenvolvimento)
                services.AddDbContextPool<BancoContexto>(options =>
                        options.UseSqlite(Configuracoes.ConnString)
                    );
            else
                services.AddDbContextPool<BancoContexto>(options =>
                        options.UseSqlServer(Configuracoes.ConnString)
            );

        }

        public static void ConfigurarDal(this IServiceCollection services)
        {
            // Repositórios
            services.AddTransient<IUow, Uow>();
            services.AddTransient<ISemeadorBd, SemeadorBd>();
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IMinisterioRepositorio, MinisterioRepositorio>();
            services.AddTransient<IInstrumentoRepositorio, InstrumentoRepositorio>();
            services.AddTransient<IEscalaRepositorio, EscalaRepositorio>();
            services.AddTransient<IMusicaRepositorio, MusicaRepositorio>();
            services.AddTransient<IDispositivoRepositorio, DispositivoRepositorio>();
        }
    }
}
