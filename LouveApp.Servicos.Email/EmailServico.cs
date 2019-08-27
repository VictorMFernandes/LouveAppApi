using LouveApp.Dominio.Servicos;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LouveApp.Servicos.Email
{
    public class EmailServico : IEmailServico
    {
        private readonly SendGridClient _cliente;
        private readonly EmailAddress _emailOrigem;

        public EmailServico(string apiKey, string emailOrigem, string nomeOrigem)
        {
            _cliente = new SendGridClient(apiKey);
            _emailOrigem = new EmailAddress(emailOrigem, nomeOrigem);
        }

        public async void EnviarBoasVindas(string enderecoEmailDestino, string nomeUsuarioDestino)
        {
            return;
            var emailDestino = new EmailAddress(enderecoEmailDestino, nomeUsuarioDestino);
            var subject = "Hello world email from Sendgrid ";
            var plainTextContent = "Plain text content";
            var htmlContent = "<strong>Hello world with HTML content</strong>";
            var msg = MailHelper.CreateSingleEmail(_emailOrigem, emailDestino, subject, plainTextContent, htmlContent);
            var response = await _cliente.SendEmailAsync(msg);
        }
    }
}
