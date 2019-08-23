using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Dominio.Entidades;

namespace LouveApp.Dominio.Servicos
{
    public interface IPushNotificationServico
    {
        void NotificarIngressoEmMinisterio(List<string> administradoresTokens, string nomeIngressante, Ministerio ministerio);
        Task NotificarIngressoEmEscala(List<string> aparelhosTokens, string dataEscala, string nomeMinisterio);
        void NotificarChatMinisterio(List<string> aparelhosTokens, string mensagem, Usuario rementente, Ministerio ministerio);
    }
}
