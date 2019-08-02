using LouveApp.Dominio.Servicos;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class EmailServicoFalso : IEmailServico
    {
        public void Enviar(string para, string de, string assunto, string corpo) { }
    }
}
