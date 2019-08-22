using System.Threading.Tasks;

namespace LouveApp.Dominio.Servicos
{
    public interface IPushNotificationServico
    {
        Task NotificarIngressoEmMinisterio(string[] aparelhosTokens, string nomeIngressante, string nomeMinisterio);
    }
}
