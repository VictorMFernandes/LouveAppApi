using LouveApp.Dominio.Servicos;

namespace LouveApp.Testes.Falsos
{
    internal class EmailServicoFalso : IEmailServico
    {
        public void Enviar(string para, string de, string assunto, string corpo)
        {
            
        }
    }
}
