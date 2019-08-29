using System.Collections.Generic;
using LouveApp.Dominio.Entidades;

namespace LouveApp.Dominio.Servicos
{
    public interface IPushNotificationServico
    {
        void NotificarIngressoEmMinisterio(List<string> administradoresTokens, string nomeIngressante, Ministerio ministerio);
        void NotificarIngressoEmEscala(List<string> aparelhosTokens, string dataEscala, Ministerio ministerio);
        void NotificarChatMinisterio(List<string> aparelhosTokens, string mensagem, Usuario rementente, Ministerio ministerio);
    }
}
