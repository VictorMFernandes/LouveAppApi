using LouveApp.Dominio.Servicos;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class EmailServicoFalso : IEmailServico
    {
        public void EnviarBoasVindas(string emailDestino, string nomeDestino) { }
    }
}
