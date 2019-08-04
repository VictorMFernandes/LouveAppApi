using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.Servicos;
using LouveApp.Infra.Servicos.Email.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LouveApp.Infra.Servicos.Email.Extensoes
{
    public static class EmailExtensoes
    {
        public static void ConfigurarServicoEmail(this IServiceCollection services, IConfiguration config)
        {
            var apiKey = config.GetSection(EConfigEmailSecao.ApiKey.EnumTextos().Nome).Value;
            var emailRemetente = config.GetSection(EConfigEmailSecao.EmailRemetente.EnumTextos().Nome).Value;
            var nomeRemetente = config.GetSection(EConfigEmailSecao.NomeRemetente.EnumTextos().Nome).Value;

            services.AddSingleton<IEmailServico>(x => new EmailServico(apiKey, emailRemetente, nomeRemetente));
        }
    }
}
