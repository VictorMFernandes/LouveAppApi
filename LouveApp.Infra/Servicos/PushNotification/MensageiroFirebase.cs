using System.Collections.Generic;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace LouveApp.Infra.Servicos.PushNotification
{
    internal class MensageiroFirebase
    {
        private readonly FirebaseMessaging _mensageiro;

        public MensageiroFirebase(string adminKeyCaminho, string integracaoUrl)
        {
            var app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(adminKeyCaminho).CreateScoped(integracaoUrl)
            });

            _mensageiro = FirebaseMessaging.GetMessaging(app);
        }

        public void EnviarNotificacao(string aparelhoToken, string titulo, string corpo, Dictionary<string, string> data)
        {
            _mensageiro.SendAsync(CriarNotificacao(aparelhoToken, titulo, corpo, data));
        }

        private static Message CriarNotificacao(string aparelhoToken, string titulo, string corpo, Dictionary<string, string> data)
        {
            return new Message
            {
                Token = aparelhoToken,
                Notification = new Notification
                {
                    Body = corpo,
                    Title = titulo
                },
                Data = data
            };
        }
    }
}
