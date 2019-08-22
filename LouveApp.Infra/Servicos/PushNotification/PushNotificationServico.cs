using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using LouveApp.Dominio.Servicos;
using System.Threading.Tasks;

namespace LouveApp.Infra.Servicos.PushNotification
{
    public class PushNotificationServico : IPushNotificationServico
    {
        private readonly FirebaseMessaging messaging;

        public PushNotificationServico()
        {
            var app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("firebaseAdminKey.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging")
            });

            messaging = FirebaseMessaging.GetMessaging(app);
        }

        public async Task NotificarIngressoEmMinisterio(string[] aparelhosTokens, string nomeIngressante, string nomeMinisterio)
        {
            await SendNotification(aparelhosTokens[0], "Meu título", "Corpo da mensagem");
        }

        public async Task SendNotification(string token, string title, string body)
        {
            var result = await messaging.SendAsync(CreateNotification(title, body, token));
            //do something with result
            var algo = string.Empty;
        }

        private Message CreateNotification(string title, string notificationBody, string token)
        {
            return new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title
                }
            };
        }
    }
}
