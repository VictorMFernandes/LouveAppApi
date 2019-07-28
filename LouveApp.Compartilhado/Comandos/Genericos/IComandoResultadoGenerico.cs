using System.Net;

namespace LouveApp.Compartilhado.Comandos.Genericos
{
    public interface IComandoResultadoGenerico : IComandoResultado
    {
        HttpStatusCode CodigoHttp { get; }
        string Resultado { get; }
        bool Sucesso { get; }
    }
}
