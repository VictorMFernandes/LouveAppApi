using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Dominio.Entidades;

namespace LouveApp.Dominio.Servicos
{
    public interface IPushNotificationServico
    {
        Task NotificarIngressoEmMinisterio(string aparelhoToken, string nomeIngressante, string nomeMinisterio);
        Task NotificarIngressoEmEscala(List<string> aparelhosTokens, string dataEscala, string nomeMinisterio);
        void NotificarChatMinisterio(List<string> aparelhosTokens, string mensagem, Usuario rementente, string nomeMinisterio);
    }
}
