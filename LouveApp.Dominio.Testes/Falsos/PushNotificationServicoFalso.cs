using System.Threading.Tasks;
using LouveApp.Dominio.Servicos;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class PushNotificationServicoFalso : IPushNotificationServico
    {
        public Task NotificarIngressoEmMinisterio(string[] aparelhosTokens, string nomeIngressante, string nomeMinisterio)
        {
            return null;
        }
    }
}
