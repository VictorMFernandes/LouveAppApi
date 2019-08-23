using System.Collections.Generic;
using LouveApp.Dominio.Servicos;
using System.Threading.Tasks;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Entidades;

namespace LouveApp.Infra.Servicos.PushNotification
{
    public class PushNotificationServico : IPushNotificationServico
    {
        private readonly MensageiroFirebase _mensageiro;

        public PushNotificationServico()
        {
            _mensageiro = new MensageiroFirebase("firebaseAdminKey.json", "https://www.googleapis.com/auth/firebase.messaging");
        }

        public async Task NotificarIngressoEmMinisterio(string aparelhoToken, string nomeIngressante, string nomeMinisterio)
        {
            var corpo = string.Format(PadroesMensagens.IngressoEmMinisterioCorpo, nomeIngressante, nomeMinisterio);

            await _mensageiro.EnviarNotificacao(aparelhoToken, PadroesMensagens.IngressoEmMinisterioTitulo, corpo);
        }

        public Task NotificarIngressoEmEscala(List<string> aparelhosTokens, string dataEscala, string nomeMinisterio)
        {
            return null;
        }

        public void NotificarChatMinisterio(List<string> aparelhosTokens, string mensagem, Usuario rementente, string nomeMinisterio)
        {
            var corpo = $"{rementente}: {mensagem}";

            foreach (var aparelhoToken in aparelhosTokens)
            {
                _mensageiro.EnviarNotificacao(aparelhoToken, nomeMinisterio, corpo);
            }
        }
    }
}
