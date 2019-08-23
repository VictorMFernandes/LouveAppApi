using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Servicos;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class PushNotificationServicoFalso : IPushNotificationServico
    {
        public void NotificarIngressoEmMinisterio(List<string> administradoresTokens, string nomeIngressante, Ministerio ministerio)
        {
        }

        public Task NotificarIngressoEmEscala(List<string> aparelhosTokens, string dataEscala, string nomeMinisterio)
        {
            return null;
        }

        public void NotificarChatMinisterio(List<string> aparelhosTokens, string mensagem, Usuario rementente, Ministerio ministerio)
        {
        }
    }
}
