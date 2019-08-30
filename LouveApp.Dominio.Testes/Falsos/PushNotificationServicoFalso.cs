using System.Collections.Generic;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Servicos;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class PushNotificationServicoFalso : IPushNotificationServico
    {
        public void NotificarIngressoEmMinisterio(List<string> administradoresTokens, string nomeIngressante, Ministerio ministerio)
        {
        }

        public void NotificarChatMinisterio(List<string> aparelhosTokens, string mensagem, Usuario rementente, Ministerio ministerio)
        {
        }

        public void NotificarIngressoEmEscala(List<string> aparelhosTokens, string dataEscala, Ministerio ministerio)
        {
        }
    }
}
