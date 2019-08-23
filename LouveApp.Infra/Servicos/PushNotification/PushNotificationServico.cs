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

        public void NotificarIngressoEmMinisterio(List<string> administradoresTokens, string nomeIngressante, Ministerio ministerio)
        {
            var corpo = string.Format(PadroesMensagens.IngressoEmMinisterioCorpo, nomeIngressante);
            var data = new Dictionary<string, string>
            {
                {"tipo", "novo_ingressante" } ,
                {"ministerioId", ministerio.Id }
            };

            foreach (var aparelhoToken in administradoresTokens)
            {
                _mensageiro.EnviarNotificacao(aparelhoToken, ministerio.ToString(), corpo, data);
            }
        }

        public Task NotificarIngressoEmEscala(List<string> aparelhosTokens, string dataEscala, string nomeMinisterio)
        {
            return null;
        }

        public void NotificarChatMinisterio(List<string> aparelhosTokens, string mensagem, Usuario rementente, Ministerio ministerio)
        {
            var corpo = $"{rementente}: {mensagem}";
            var data = new Dictionary<string, string>
            {
                {"tipo", "nova_mensagem_ministerio" } ,
                {"ministerioId", ministerio.Id }
            };

            foreach (var aparelhoToken in aparelhosTokens)
            {
                _mensageiro.EnviarNotificacao(aparelhoToken, ministerio.ToString(), corpo, data);
            }
        }
    }
}
