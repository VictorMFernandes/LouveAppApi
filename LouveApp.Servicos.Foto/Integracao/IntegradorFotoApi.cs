using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.Servicos;
using LouveApp.Servicos.Fotos.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LouveApp.Servicos.Fotos.Integracao
{
    public static class IntegradorFotoApi
    {
        public static void ConfigurarServicoFotos(this IServiceCollection services, IConfiguration config)
        {
            var cloudName = config.GetSection(EConfigFotosSecao.CloudName.EnumTextos().Nome).Value;
            var apiKey = config.GetSection(EConfigFotosSecao.ApiKey.EnumTextos().Nome).Value;
            var apiSecret = config.GetSection(EConfigFotosSecao.ApiSecret.EnumTextos().Nome).Value;

            services.AddSingleton<IFotoServico>(x => new FotoServico(cloudName, apiKey, apiSecret));
        }
    }
}
