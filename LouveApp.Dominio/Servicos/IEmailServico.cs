namespace LouveApp.Dominio.Servicos
{
    public interface IEmailServico
    {
        void EnviarBoasVindas(string emailDestino, string nomeDestino);
    }
}
