using LouveApp.Dominio.Comandos.AutenticacaoComandos.Entradas;
using LouveApp.Dominio.Sistema.Exemplos;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Api.Documentacao.Exemplos
{
    public class AutenticarUsuarioComandoExemplo : IExamplesProvider<AutenticarUsuarioComando>
    {
        public AutenticarUsuarioComando GetExamples()
        {
            return ExemplosComando.AutenticarUsuario;
        }
    }
}
