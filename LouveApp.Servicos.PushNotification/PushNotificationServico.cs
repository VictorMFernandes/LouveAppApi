﻿using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Servicos;
using LouveApp.Dominio.Sistema.Padroes;
using LouveApp.Servicos.PushNotification.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LouveApp.Servicos.PushNotification
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
