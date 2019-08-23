using System.Threading.Tasks;
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

        public async Task EnviarNotificacao(string aparelhoToken, string titulo, string corpo)
        {
            var result = await _mensageiro.SendAsync(CriarNotificacao(titulo, corpo, aparelhoToken));
        }

        private static Message CriarNotificacao(string aparelhoToken, string titulo, string corpo)
        {
            return new Message
            {
                Token = aparelhoToken,
                Notification = new Notification
                {
                    Body = corpo,
                    Title = titulo
                }
            };
        }
    }
}
