using System.Net;

namespace LouveApp.Compartilhado.Comandos.Genericos
{
    public class NaoAutorizadoResultado : IComandoResultadoGenerico
    {
        public string Resultado { get; }

        public HttpStatusCode CodigoHttp => HttpStatusCode.Forbidden;

        public bool Sucesso => false;

        public NaoAutorizadoResultado(string descricao)
        {
            Resultado = descricao;
        }
    }
}
