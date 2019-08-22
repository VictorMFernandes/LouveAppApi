using LouveApp.Dominio.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace LouveApp.Infra.Servicos.PushNotification.Extensoes
{
    public static class PushNotificationExtensoes
    {
        public static void ConfigurarPushNotification(this IServiceCollection services)
        {
            services.AddSingleton<IPushNotificationServico>(x => new PushNotificationServico());
        }
    }
}
