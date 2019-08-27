using LouveApp.Dominio.Servicos;
using LouveApp.Servicos.PushNotification;
using Microsoft.Extensions.DependencyInjection;

namespace LouveApp.Dal.Servicos.PushNotification.Extensoes
{
    public static class IntegradorPushNotificationApi
    {
        public static void ConfigurarPushNotification(this IServiceCollection services)
        {
            services.AddSingleton<IPushNotificationServico>(x => new PushNotificationServico());
        }
    }
}
